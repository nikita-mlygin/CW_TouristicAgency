namespace NCourseWork.Persistence.Purchase
{
    using Dapper;
    using MapsterMapper;
    using NCourseWork.Domain.Country;
    using NCourseWork.Domain.Hotel;
    using NCourseWork.Domain.Purchase;
    using NCourseWork.Persistence.Client.Get;
    using NCourseWork.Persistence.Common.Compiler;
    using NCourseWork.Persistence.Common.Database;
    using NCourseWork.Persistence.Purchase.Get;
    using NCourseWork.Persistence.Tour.Get;
    using SqlKata;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal class PurchaseRepository : IPurchaseRepository
    {
        private readonly IDbConnectionProvider connectionProvider;
        private readonly ISqlCompilerProvider sqlCompilerProvider;
        private readonly IMapper mapper;

        public PurchaseRepository(IDbConnectionProvider connectionProvider, ISqlCompilerProvider sqlCompilerProvider, IMapper mapper)
        {
            this.connectionProvider = connectionProvider;
            this.sqlCompilerProvider = sqlCompilerProvider;
            this.mapper = mapper;
        }

        public async Task AddPurchaseAsync(Purchase purchase)
        {
            using var conn = await connectionProvider.GetConnectionAsync();
            var compiler = sqlCompilerProvider.GetCompiler();

            var query = compiler.Compile(new Query("Purchase")
                .AsInsert(new
                {
                    purchase.Id,
                    ClientId = purchase.Client.Id,
                    TourId = purchase.Tour.Id,
                    purchase.WeekCount,
                    purchase.TotalPrice,
                    purchase.TotalDiscount,
                    purchase.PurchaseDate,
                }));

            await conn.ExecuteAsync(query.Sql, query.NamedBindings);
        }

        public async Task<Purchase?> GetPurchaseByIdAsync(Guid purchaseId)
        {
            using var conn = await connectionProvider.GetConnectionAsync();
            var compiler = sqlCompilerProvider.GetCompiler();

            var query = compiler.Compile(new Query("Purchase")
                .Select(
                    "Purchase.Id",
                    "Purchase.PurchaseDate",
                    "Purchase.TotalPrice",
                    "Purchase.TotalDiscount",
                    "Purchase.WeekCount",
                    "Client.Id as ClientId",
                    "Client.FirstName",
                    "Client.LastName",
                    "Client.MiddleName",
                    "Client.Address",
                    "Client.PhoneNumber",
                    "Status.Id as StatusId",
                    "Status.StatusName",
                    "Status.DiscountPercentage",
                    "Tour.Id as TourId",
                    "Tour.PricePerWeek",
                    "Hotel.Id as HotelId",
                    "Hotel.HotelName",
                    "Hotel.HotelClass",
                    "Country.Id as CountryId",
                    "Country.CountryName",
                    "Country.Climate")
                .Join("Client", "Client.Id", "Purchase.ClientId")
                .Join("ClientStatus", "Client.Id", "ClientStatus.ClientId")
                .Join("Status", "ClientStatus.StatusId", "Status.Id")
                .Join("Tour", "Tour.Id", "Purchase.TourId")
                .Join("Hotel", "Hotel.Id", "Tour.HotelId")
                .Join("Country", "Country.Id", "Hotel.CountryId")
                .Where("Purchase.Id", purchaseId.ToByteArray()));

            var response = await conn.QuerySingleOrDefaultAsync<PurchaseGetFullResponse?>(query.Sql, query.NamedBindings);

            return response is null ? null : mapper.Map<Purchase>(response);
        }

        public async Task<IEnumerable<Purchase>> GetWithFilterAsync(Guid? clientFilter, DateTime? startDateTimeFiller, DateTime? endDateTimeFiller)
        {
            using var conn = await connectionProvider.GetConnectionAsync();
            var compiler = sqlCompilerProvider.GetCompiler();

            var queryObj = new Query("Purchase")
                .Select("Purchase.Id as PurchaseId",
                        "Purchase.WeekCount",
                        "Purchase.PurchaseDate",
                        "Client.Id as ClientId",
                        "Client.FirstName",
                        "Client.LastName",
                        "Client.MiddleName",
                        "Tour.Id as TourId",
                        "Country.Id as CountryId",
                        "Country.CountryName",
                        "Tour.PricePerWeek",
                        "Hotel.Id as HotelId",
                        "Hotel.HotelName")
                .Join("Client", "Purchase.ClientId", "Client.Id")
                .Join("Tour", "Purchase.TourId", "Tour.Id")
                .Join("Hotel", "Tour.HotelId", "Hotel.Id")
                .Join("Country", "Hotel.CountryId", "Country.Id");

            if (clientFilter is not null)
            {
                queryObj.Where("Client.Id", clientFilter?.ToByteArray());
            }

            if (startDateTimeFiller is not null && endDateTimeFiller is not null)
            {
                queryObj.WhereBetween("Purchase.PurchaseDate", startDateTimeFiller, endDateTimeFiller);
            } else
            {
                if (startDateTimeFiller is not null)
                {
                    queryObj.Where("Purchase.PurchaseDate", ">=", startDateTimeFiller);
                }

                if (endDateTimeFiller is not null)
                {
                    queryObj.Where("Purchase.PurchaseDate", "<=", endDateTimeFiller);
                }
            }

            var query = compiler.Compile(queryObj);

            var response = await conn.QueryAsync<PurchaseListInfoResponse, GetClientListItemResponse, AllTourResponse, PurchaseListInfoResponse>(query.Sql, (purchase, client, tour) =>
            {
                client.Id = purchase.ClientId;
                tour.Id = purchase.TourId;

                purchase.ClientInfo = client;
                purchase.TourInfo = tour;

                return purchase;
            }, query.NamedBindings, splitOn: "ClientId, TourId");

            return mapper.Map<IEnumerable<Purchase>>(response);
        }

        public async Task RemovePurchaseAsync(Guid purchaseId)
        {
            using var conn = await connectionProvider.GetConnectionAsync();
            var compiler = sqlCompilerProvider.GetCompiler();

            var query = compiler.Compile(new Query("Purchase")
                .AsDelete()
                .Where("Id", purchaseId.ToByteArray()));

            await conn.ExecuteAsync(query.Sql, query.NamedBindings);
        }

        public async Task UpdatePurchaseAsync(Purchase purchase)
        {
            using var conn = await connectionProvider.GetConnectionAsync();
            var compiler = sqlCompilerProvider.GetCompiler();

            var query = compiler.Compile(new Query("Purchase")
                .AsUpdate(new
                {
                    purchase.PurchaseDate,
                    purchase.TotalPrice,
                    purchase.TotalDiscount,
                    purchase.WeekCount,
                    ClientId = purchase.Client.Id,
                    TourId = purchase.Tour.Id,
                })
                .Where("Id", purchase.Id.ToByteArray()));

            await conn.ExecuteAsync(query.Sql, query.NamedBindings);
        }
    }
}
