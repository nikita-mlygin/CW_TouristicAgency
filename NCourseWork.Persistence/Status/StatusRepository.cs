namespace NCourseWork.Persistence.Status
{
    using NCourseWork.Domain.Status;
    using System;
    using System.Data;
    using System.Threading.Tasks;
    using Dapper;
    using NCourseWork.Persistence.Common.Database;
    using SqlKata;
    using SqlKata.Compilers;
    using NCourseWork.Persistence.Common.Compiler;

    internal class StatusRepository : IStatusRepository
    {
        private readonly IDbConnectionProvider connectionProvider;
        private readonly ISqlCompilerProvider sqlCompilerProvider;

        public StatusRepository(IDbConnectionProvider connectionProvider, ISqlCompilerProvider sqlCompilerProvider)
        {
            this.connectionProvider = connectionProvider;
            this.sqlCompilerProvider = sqlCompilerProvider;
        }

        public async Task AddStatusAsync(Status status)
        {
            using var conn = await connectionProvider.GetConnectionAsync();
            var compiler = sqlCompilerProvider.GetCompiler();

            var query = compiler.Compile(new Query("Status")
                .AsInsert(new
                {
                    status.Id,
                    status.StatusName,
                    status.DiscountPercentage
                }));

            await conn.ExecuteAsync(query.Sql, query.NamedBindings);
        }

        public async Task<Status> GetFirstStatusIdAsync()
        {
            using var conn = await connectionProvider.GetConnectionAsync();
            var compiler = sqlCompilerProvider.GetCompiler();

            var query = compiler.Compile(new Query("Status")
                .Select("Id")
                .OrderBy("Id")
                .Limit(1));

            var result = await conn.QueryFirstOrDefaultAsync<Status>(query.Sql, query.NamedBindings);

            return result == null ? throw new DataException("No status records found.") : result;
        }

        public async Task<Status?> GetStatusByIdAsync(Guid statusId)
        {
            using var conn = await connectionProvider.GetConnectionAsync();
            var compiler = sqlCompilerProvider.GetCompiler();

            var query = compiler.Compile(new Query("Status")
                .Where("Id", statusId));

            return await conn.QueryFirstOrDefaultAsync<Status>(query.Sql, query.NamedBindings);
        }

        public async Task<Status> GetStatusForPurchaseCount(int count)
        {
            using var conn = await connectionProvider.GetConnectionAsync();
            var compiler = sqlCompilerProvider.GetCompiler();

            var query = compiler.Compile(new Query("Status")
                .Select("Id", "StatusName")
                .Where("MinimalOrderCount", "<", count)
                .OrderByDesc("MinimalOrderCount")
                .Limit(1));

            return await conn.QueryFirstAsync<Status>(query.Sql, query.NamedBindings);
        }

        public async Task RemoveStatusAsync(Guid statusId)
        {
            using var conn = await connectionProvider.GetConnectionAsync();
            var compiler = sqlCompilerProvider.GetCompiler();

            var query = compiler.Compile(new Query("Status")
                .AsDelete()
                .Where("Id", statusId));

            await conn.ExecuteAsync(query.Sql, query.NamedBindings);
        }

        public async Task UpdateStatusAsync(Status status)
        {
            using var conn = await connectionProvider.GetConnectionAsync();
            var compiler = sqlCompilerProvider.GetCompiler();

            var query = compiler.Compile(new Query("Status")
                .AsUpdate(new
                {
                    status.StatusName,
                    status.DiscountPercentage
                })
                .Where("Id", status.Id));

            await conn.ExecuteAsync(query.Sql, query.NamedBindings);
        }
    }
}
