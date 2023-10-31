namespace NCourseWork.Persistence.Hotel
{
    using Dapper;
    using MapsterMapper;
    using NCourseWork.Domain.Hotel;
    using NCourseWork.Persistence.Common.Compiler;
    using NCourseWork.Persistence.Common.Database;
    using NCourseWork.Persistence.Hotel.Get;
    using SqlKata;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class HotelRepository : IHotelRepository
    {
        private readonly IMapper mapper;
        private readonly IDbConnectionProvider connectionProvider;
        private readonly ISqlCompilerProvider compilerProvider;

        public HotelRepository(IMapper mapper, IDbConnectionProvider connectionProvider, ISqlCompilerProvider compilerProvider)
        {
            this.mapper = mapper;
            this.connectionProvider = connectionProvider;
            this.compilerProvider = compilerProvider;
        }
        
        public async Task AddHotelAsync(Hotel hotel)
        {
            using var conn = await connectionProvider.GetConnectionAsync();
            var compiler = compilerProvider.GetCompiler();
            var query = compiler.Compile(new Query("Hotel")
                .AsInsert(new
                {
                    hotel.Id,
                    hotel.HotelName,
                    hotel.HotelClass,
                    CountryId = hotel.Country.Id,
                }));

            await conn.ExecuteAsync(query.Sql, query.NamedBindings);
        }

        public async Task<Hotel?> GetHotelByIdAsync(Guid hotelId)
        {
            using var conn = await connectionProvider.GetConnectionAsync();
            var compiler = compilerProvider.GetCompiler();
            var query = compiler.Compile(new Query("Hotel")
                .Select("Hotel.Id", "Hotel.HotelName", "Hotel.HotelClass", "Hotel.CountryId", "Country.Id as CountryId", "Country.CountryName", "Country.Climate")
                .Join("Country", "Country.Id", "Hotel.CountryId")
                .Where("Hotel.Id", hotelId.ToByteArray()));

            var response = await conn.QuerySingleOrDefaultAsync<GetHotelByIdResponse?>(query.Sql, query.NamedBindings);

            if (response is null)
            {
                return null;
            }

            var res = mapper.Map<Hotel>(response);
            res.Id = hotelId;

            return res;
        }

        public async Task DeleteHotelAsync(Guid hotelId)
        {
            using var conn = await connectionProvider.GetConnectionAsync();
            var compiler = compilerProvider.GetCompiler();
            var query = compiler.Compile(new Query("Hotel")
                .AsDelete()
                .Where("Id", hotelId.ToByteArray()));

            await conn.ExecuteAsync(query.Sql, query.NamedBindings);
        }

        public async Task UpdateHotelAsync(Hotel hotel)
        {
            using var conn = await connectionProvider.GetConnectionAsync();
            var compiler = compilerProvider.GetCompiler();
            var query = compiler.Compile(new Query("Hotel")
                .AsUpdate(new
                {
                    hotel.HotelName,
                    hotel.HotelClass,
                    CountryId = hotel.Country.Id,
                })
                .Where("Id", hotel.Id.ToByteArray()));

            await conn.ExecuteAsync(query.Sql, query.NamedBindings);
        }

        public async Task<IEnumerable<Hotel>> GetHotelByNameFilter(string filter)
        {
            using var conn = await connectionProvider.GetConnectionAsync();
            var compiler = compilerProvider.GetCompiler();
            var query = compiler.Compile(new Query("Hotel")
                .Select("Id", "HotelName")
                .WhereLike("HotelName", $"%{filter}%"));

            return await conn.QueryAsync<Hotel>(query.Sql, query.NamedBindings);
        }

        public async Task<IEnumerable<Hotel>> GetAllAsync()
        {
            using var conn = await connectionProvider.GetConnectionAsync();
            var compiler = compilerProvider.GetCompiler();
            var query = compiler.Compile(new Query("Hotel")
                .Select("Id", "HotelName"));

            return await conn.QueryAsync<Hotel>(query.Sql, query.NamedBindings);
        }

        public async Task<IEnumerable<Hotel>> GetWithFilterAsync(string? nameFilter, Guid? countryFilter, HotelClass? hotelClassFilter)
        {
            using var conn = await connectionProvider.GetConnectionAsync();
            var compiler = compilerProvider.GetCompiler();
            var queryObj = new Query("Hotel")
                .Select("Id", "HotelName");

            if (nameFilter is not null)
            {
                queryObj.WhereLike("HotelName", $"%{nameFilter}%");
            }

            if (countryFilter is not null)
            {
                queryObj.Where("CountryId", countryFilter?.ToByteArray());
            }

            if (hotelClassFilter is not null)
            {
                queryObj.Where("HotelClass", hotelClassFilter);
            }

            var query = compiler.Compile(queryObj);

            return await conn.QueryAsync<Hotel>(query.Sql, query.NamedBindings);
        }
    }
}
