namespace NCourseWork.Persistence.Country
{
    using Dapper;
    using Dapper.Transaction;
    using MapsterMapper;
    using NCourseWork.Domain.Country;
    using NCourseWork.Persistence.Common.Compiler;
    using NCourseWork.Persistence.Common.Database;
    using NCourseWork.Persistence.Country.Get;
    using SqlKata;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class CountryRepository : ICountryRepository
    {
        private readonly IDbConnectionProvider connectionProvider;
        private readonly ISqlCompilerProvider sqlCompilerProvider;
        private readonly IMapper mapper;

        public CountryRepository(IDbConnectionProvider connectionProvider, ISqlCompilerProvider sqlCompilerProvider, IMapper mapper)
        {
            this.connectionProvider = connectionProvider;
            this.sqlCompilerProvider = sqlCompilerProvider;
            this.mapper = mapper;
        }

        public async Task AddCountryAsync(Country country)
        {
            using var conn = await connectionProvider.GetConnectionAsync();
            var compiler = sqlCompilerProvider.GetCompiler();

            var query = compiler.Compile(new Query("Country")
                .AsInsert(new
                {
                    country.Id,
                    country.Info.CountryName,
                    country.Info.Climate,
                }));

            await conn.ExecuteAsync(query.Sql, query.NamedBindings);
        }

        public async Task DeleteCountryAsync(Guid countryId)
        {
            using var conn = await connectionProvider.GetConnectionAsync();
            var compiler = sqlCompilerProvider.GetCompiler();


            var query = compiler.Compile(new Query("Country")
                .AsDelete()
                .Where("Id", countryId));

            await conn.ExecuteAsync(query.Sql, query.NamedBindings);
        }

        public async Task UpdateCountryAsync(Country country)
        {
            using var conn = await connectionProvider.GetConnectionAsync();
            var compiler = sqlCompilerProvider.GetCompiler();

            var query = compiler.Compile(new Query("Country")
                .AsUpdate(
                    new
                    {
                        country.Info.CountryName,
                        country.Info.Climate,
                    }
                )
                .Where("Id", country.Id));

            await conn.ExecuteAsync(query.Sql, query.NamedBindings);
        }

        public async Task<Country?> GetCountryByIdAsync(Guid countryId)
        {
            using var conn = await connectionProvider.GetConnectionAsync();
            var compiler = sqlCompilerProvider.GetCompiler();

            var query = compiler.Compile(new Query("Country")
                .Select("CountryName", "Climate")
                .Where("Id", countryId));

            var response = await conn.QueryFirstOrDefaultAsync<GetCountryByIdResponse?>(query.Sql, query.NamedBindings);

            if (response is null) { return null; }

            var res = mapper.Map<Country>(response);
            res.Id = countryId;

            return res;
        }

        public async Task<IEnumerable<Country>> GetWithFilterAsync(string? nameFilter, string? climateNameFilter)
        {
            using var conn = await connectionProvider.GetConnectionAsync();
            var compiler = sqlCompilerProvider.GetCompiler();

            var queryObj = new Query("Country")
                .Select("Id", "CountryName");

            if (climateNameFilter is not null)
            {
                queryObj.WhereLike("Climate", $"%{climateNameFilter}%");
            }

            if (nameFilter is not null)
            {
                queryObj.WhereLike("CountryName", $"%{nameFilter}%");
            }

            var query = compiler.Compile(queryObj);

            return mapper.Map<IEnumerable<Country>>(conn.Query<GetCountryByNameResponse>(query.Sql, query.NamedBindings));
        }

        public async Task<IEnumerable<Country>> GetAll()
        {
            using var conn = await connectionProvider.GetConnectionAsync();
            var compiler = sqlCompilerProvider.GetCompiler();

            var query = compiler.Compile(new Query("Country")
                .Select("Id", "CountryName"));

            return mapper.Map<IEnumerable<Country>>(conn.Query<GetCountryByNameResponse>(query.Sql, query.NamedBindings));
        }
    }
}
