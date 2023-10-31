namespace NCourseWork.Persistence.Tour
{
    using Dapper;
    using MapsterMapper;
    using NCourseWork.Domain.Country;
    using NCourseWork.Domain.Hotel;
    using NCourseWork.Domain.Tour;
    using NCourseWork.Persistence.Common.Compiler;
    using NCourseWork.Persistence.Common.Database;
    using NCourseWork.Persistence.Tour.Get;
    using SqlKata;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Metrics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class TourRepository : ITourRepository
    {
        private readonly IDbConnectionProvider connectionProvider;
        private readonly ISqlCompilerProvider sqlCompilerProvider;
        private readonly IMapper mapper;

        public TourRepository(IDbConnectionProvider connectionProvider, ISqlCompilerProvider sqlCompilerProvider, IMapper mapper)
        {
            this.connectionProvider = connectionProvider;
            this.sqlCompilerProvider = sqlCompilerProvider;
            this.mapper = mapper;
        }

        public async Task AddTourAsync(Tour tour)
        {
            using var conn = await connectionProvider.GetConnectionAsync();
            var compiler = sqlCompilerProvider.GetCompiler();

            var query = compiler.Compile(new Query("Tour")
                .AsInsert(new
                {
                    tour.Id,
                    HotelId = tour.Hotel.Id,
                    tour.PricePerWeek
                }));

            await conn.ExecuteAsync(query.Sql, query.NamedBindings);
        }

        public async Task<IEnumerable<Tour>> GetAll()
        {
            using var conn = await connectionProvider.GetConnectionAsync();
            var compiler = sqlCompilerProvider.GetCompiler();

            var query = compiler.Compile(new Query("Tour")
                .Select("Tour.Id", "Tour.PricePerWeek", "Hotel.Id as HotelId", "Hotel.HotelName", "Country.CountryName", "Country.Id as CountryId")
                .Join("Hotel", "Hotel.Id", "Tour.HotelId")
                .Join("Country", "Hotel.CountryId", "Country.Id"));

            var response = await conn.QueryAsync<AllTourResponse>(query.Sql, query.NamedBindings);

            return mapper.Map<IEnumerable<Tour>>(response);
        }

        public async Task<IEnumerable<Tour>> GetByCountryAsync(Country country)
        {
            using var conn = await connectionProvider.GetConnectionAsync();
            var compiler = sqlCompilerProvider.GetCompiler();

            var query = compiler.Compile(new Query("Tour")
                .Select("Tour.Id", "Tour.PricePerWeek", "Hotel.Id as HotelId", "Hotel.HotelName")
                .Join("Hotel", "Hotel.Id", "Tour.HotelId")
                .Where("Hotel.CountryId", country.Id.ToByteArray()));

            var response = await conn.QueryAsync<TourByCountryResponse>(query.Sql, query.NamedBindings);

            var res = mapper.Map<IEnumerable<Tour>>(response);

            if (country.Info?.CountryName is null)
            {
                query = compiler.Compile(new Query("Country")
                    .Select("CountryName")
                    .Where("Id", country.Id.ToByteArray()));

                country.Info ??= new();

                country.Info.CountryName = await conn.QuerySingleAsync<string>(query.Sql, query.NamedBindings);
            }

            var resList = res.ToList();

            resList.ForEach(x => x.Hotel.Country = country);

            return resList;
        }

        public async Task<Tour?> GetTourByIdAsync(Guid tourId)
        {
            using var conn = await connectionProvider.GetConnectionAsync();
            var compiler = sqlCompilerProvider.GetCompiler();

            var query = compiler.Compile(new Query("Tour")
                .Select("Tour.Id", "Tour.PricePerWeek", $"Hotel.Id as HotelId", "Hotel.HotelName", "Hotel.HotelClass", "Country.Id as CountryId", "Country.CountryName", "Country.Climate")
                .Join("Hotel", "Tour.HotelId", "Hotel.Id")
                .Join("Country", "Hotel.CountryId", "Country.Id")
                .Where("Tour.Id", tourId.ToByteArray()));

            var response = await conn.QueryFirstOrDefaultAsync<TourByIdResponse>(query.Sql, query.NamedBindings);

            return response is null ? null : mapper.Map<Tour>(response);
        }

        public async Task<IEnumerable<Tour>> GetWithFilterAsync(Guid? countryFilter, string? hotelNameFilter, HotelClass? hotelClassFilter, int? minValueFilter, int? maxValueFilter)
        {
            using var conn = await connectionProvider.GetConnectionAsync();
            var compiler = sqlCompilerProvider.GetCompiler();

            var queryObj = new Query("Tour")
                .Select("Tour.Id", "Tour.PricePerWeek", "Hotel.Id as HotelId", "Hotel.HotelName")
                .Join("Hotel", "Tour.HotelId", "Hotel.Id");


            if (countryFilter is not null)
            {
                queryObj.Where("Hotel.CountryId", countryFilter?.ToByteArray());
            }

            if (hotelNameFilter is not null)
            {
                queryObj.WhereLike("Hotel.HotelName", $"%{hotelNameFilter}%");
            }

            if (hotelClassFilter is not null)
            {
                queryObj.Where("Hotel.HotelClass", hotelClassFilter);
            }

            if (minValueFilter is not null)
            {
                queryObj.Where("Tour.PricePerWeek", ">=", minValueFilter);
            }

            if (maxValueFilter is not null)
            {
                queryObj.Where("Tour.PricePerWeek", "<=", maxValueFilter);
            }

            var query = compiler.Compile(queryObj);

            var response = await conn.QueryAsync<TourByCountryResponse>(query.Sql, query.NamedBindings);

            return mapper.Map<IEnumerable<Tour>>(response);
        }

        public async Task DeleteTourAsync(Guid tourId)
        {
            using var conn = await connectionProvider.GetConnectionAsync();
            var compiler = sqlCompilerProvider.GetCompiler();

            var query = compiler.Compile(new Query("Tour")
                .AsDelete()
                .Where("Id", tourId.ToByteArray()));

            await conn.ExecuteAsync(query.Sql, query.NamedBindings);
        }

        public async Task UpdateTourAsync(Tour tour)
        {
            using var conn = await connectionProvider.GetConnectionAsync();
            var compiler = sqlCompilerProvider.GetCompiler();

            var query = compiler.Compile(new Query("Tour")
                .AsUpdate(new
                {
                    tour.PricePerWeek,
                    HotelId = tour.Hotel.Id,
                })
                .Where("Tour.Id", tour.Id.ToByteArray()));

            await conn.ExecuteAsync(query.Sql, query.NamedBindings);
        }
    }
}
