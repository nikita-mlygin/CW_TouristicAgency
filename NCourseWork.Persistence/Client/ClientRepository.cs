namespace NCourseWork.Persistence.Client
{
    using Dapper;
    using Dapper.Transaction;
    using MapsterMapper;
    using NCourseWork.Domain.Client;
    using NCourseWork.Domain.Purchase;
    using NCourseWork.Domain.Status;
    using NCourseWork.Persistence.Client.Get;
    using NCourseWork.Persistence.Common.Compiler;
    using NCourseWork.Persistence.Common.Database;
    using SqlKata;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;

    internal class ClientRepository : IClientRepository
    {
        private readonly IDbConnectionProvider connectionProvider;
        private readonly ISqlCompilerProvider sqlCompilerProvider;
        private readonly IMapper mapper;

        public ClientRepository(IDbConnectionProvider connectionProvider, ISqlCompilerProvider sqlCompilerProvider, IMapper mapper)
        {
            this.connectionProvider = connectionProvider;
            this.sqlCompilerProvider = sqlCompilerProvider;
            this.mapper = mapper;
        }

        public async Task AddClientAsync(Client client)
        {
            using var conn = await connectionProvider.GetConnectionAsync();
            conn.Open();
            var compiler = sqlCompilerProvider.GetCompiler();

            using var transaction = conn.BeginTransaction();
            var query = compiler.Compile(new Query("Client")
            .AsInsert(new
            {
                client.Id,
                client.Info.LastName,
                client.Info.FirstName,
                client.Info.MiddleName,
                client.Info.Address,
                client.Info.PhoneNumber,
            }));

            await transaction.ExecuteAsync(query.Sql, query.NamedBindings);

            query = compiler.Compile(new Query("ClientStatus")
                .AsInsert(new
                {
                    ClientId = client.Id.ToByteArray(),
                    StatusId = client.Status.Id.ToByteArray(),
                }));

            await transaction.ExecuteAsync(query.Sql, query.NamedBindings);

            transaction.Commit();
        }  

        public async Task DeleteClientAsync(Guid clientId)
        {
            using var conn = await connectionProvider.GetConnectionAsync();
            conn.Open();
            var compiler = sqlCompilerProvider.GetCompiler();

            using var transaction = conn.BeginTransaction();

            var query = compiler.Compile(new Query("ClientStatus")
                .AsDelete()
                .Where("ClientId", clientId.ToByteArray()));

            await transaction.ExecuteAsync(query.Sql, query.NamedBindings);

            query = compiler.Compile(new Query("Client")
                .AsDelete()
                .Where("Id", clientId.ToByteArray()));

            await transaction.ExecuteAsync(query.Sql, query.NamedBindings);

            transaction.Commit();
        }

        public async Task UpdateClientAsync(Client client)
        {
            using var conn = await connectionProvider.GetConnectionAsync();
            conn.Open();
            var compiler = sqlCompilerProvider.GetCompiler();

            using var transaction = conn.BeginTransaction();

            var query = compiler.Compile(new Query("Client")
                .AsUpdate(
                    client.Info
                )
                .Where("Id", client.Id.ToByteArray()));

            await transaction.ExecuteAsync(query.Sql, query.NamedBindings);

            query = compiler.Compile(new Query("ClientStatus")
                .AsUpdate(new
                {
                    StatusId = client.Status.Id.ToByteArray(),
                    client.CountOfOrders,
                })
                .Where("ClientId", client.Id.ToByteArray()));

            await transaction.ExecuteAsync(query.Sql, query.NamedBindings);

            transaction.Commit();
        }

        public async Task<Client?> GetClientByIdAsync(Guid clientId)
        {
            using var getUserInfoConn = await connectionProvider.GetConnectionAsync();
            getUserInfoConn.Open();
            using var getUserStatusConn = await connectionProvider.GetConnectionAsync();
            getUserStatusConn.Open();
            var compiler = sqlCompilerProvider.GetCompiler();
            var query = compiler.Compile(new Query("Client")
                .Select("LastName", "FirstName", "MiddleName", "Address", "PhoneNumber")
                .Where("Id", clientId.ToByteArray()));
            var userInfoResponseTask = getUserInfoConn.QueryFirstOrDefaultAsync<GetClientByIdResponse?>(query.Sql, query.NamedBindings);

            query = compiler.Compile(new Query("Status")
                .Select("CountOfOrders", "Id", "StatusName", "DiscountPercentage")
                .Join("ClientStatus", "ClientStatus.StatusId", "Status.Id")
                .Where("ClientId", clientId.ToByteArray()));
            var userStatusTask = getUserStatusConn.QueryAsync<GetClientStatusInfoResponse, Status, GetClientStatusInfoResponse>(query.Sql, (resp, status) =>
            {
                resp.Status = status;

                return resp;
            }, query.NamedBindings, splitOn: "Id");

            await Task.WhenAll(userInfoResponseTask, userStatusTask);

            var userInfoResponse = await userInfoResponseTask;
            var userStatus = (await userStatusTask).First();

            if (userInfoResponse is null)
            {
                return null;
            }

            var result = mapper.Map<Client>(userInfoResponse);
            result.Id = clientId;
            result.Status = userStatus.Status!;
            result.CountOfOrders = userStatus.CountOfOrders;

            return result;
        }

        public async Task UpdateWithoutStatusAsync(Client client)
        {
            using var conn = await connectionProvider.GetConnectionAsync();
            var compiler = sqlCompilerProvider.GetCompiler();

            var query = compiler.Compile(new Query("Client")
                .AsUpdate(
                    client.Info
                )
                .Where("Id", client.Id.ToByteArray()));

            await conn.ExecuteAsync(query.Sql, query.NamedBindings);
        }

        public async Task<IEnumerable<Client>> GetClientsWithNameFilterAsync(string? firstName = "", string? lastName = "", string? middleName = "")
        {
            using var conn = await connectionProvider.GetConnectionAsync();
            var compiler = sqlCompilerProvider.GetCompiler();

            var queryObj = new Query("Client").Select("Id", "FirstName", "LastName", "MiddleName");

            if (firstName is not null)
            {
                queryObj.WhereLike("FirstName", $"%{firstName}%");
            }

            if (lastName is not null)
            {
                queryObj.WhereLike("LastName", $"%{lastName}%");
            }

            if (middleName is not null)
            {
                queryObj.WhereLike("MiddleName", $"%{middleName}%");
            }

            var query = compiler.Compile(queryObj);

            var response = await conn.QueryAsync<GetClientListItemResponse>(query.Sql, query.NamedBindings);

            return mapper.Map<IEnumerable<Client>>(response);
        }

        public async Task<IEnumerable<Client>> GetClientsWithOneNameFilterAsync(string name)
        {
            using var conn = await connectionProvider.GetConnectionAsync();
            var compiler = sqlCompilerProvider.GetCompiler();

            var queryObj = new Query("Client").Select("Id", "FirstName", "LastName", "MiddleName")
                .WhereLike("FirstName", $"%{name}%")
                .OrWhereLike("LastName", $"%{name}%")
                .OrWhereLike("MiddleName", $"%{name}%");

            var query = compiler.Compile(queryObj);

            var response = await conn.QueryAsync<GetClientListItemResponse>(query.Sql, query.NamedBindings);

            return mapper.Map<IEnumerable<Client>>(response);
        }

        public async Task<Client> GetClientByPurchaseAsync(Purchase purchase)
        {
            var id = purchase.Client?.Id;

            if (id is null)
            {
                using var conn = await connectionProvider.GetConnectionAsync();
                var compiler = sqlCompilerProvider.GetCompiler();

                var query = compiler.Compile(new Query("Purchase").Select("ClientId")
                    .Where("Id", purchase.Id.ToByteArray()));

                id = await conn.QuerySingleAsync<Guid>(query.Sql, query.NamedBindings);
            }

            return await this.GetClientByIdAsync((Guid)id) ?? throw new ApplicationException("Client is not found");
        }
    }
}
