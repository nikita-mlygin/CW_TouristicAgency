namespace NCourseWork.Persistence.User
{
    using Dapper;
    using NCourseWork.Domain.User;
    using NCourseWork.Persistence.Common.Compiler;
    using NCourseWork.Persistence.Common.Database;
    using SqlKata;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class UserRepository : IUserRepository
    {
        private readonly IDbConnectionProvider connectionProvider;
        private readonly ISqlCompilerProvider compilerProvider;

        public UserRepository(IDbConnectionProvider connectionProvider, ISqlCompilerProvider compilerProvider)
        {
            this.connectionProvider = connectionProvider;
            this.compilerProvider = compilerProvider;
        }

        public async Task<User?> GetByLoginAndPassword(string login, string password)
        {
            var conn = await connectionProvider.GetConnectionAsync();
            var compiler = compilerProvider.GetCompiler();

            var query = compiler.Compile(new Query("User")
                .Select("Id")
                .Where("Login", login).Where("Password", password));

            var user = await conn.QuerySingleOrDefaultAsync<User>(query.Sql, query.NamedBindings);

            if (user is null) { return null; }

            user.Login = login;

            return user;
        }
    }
}
