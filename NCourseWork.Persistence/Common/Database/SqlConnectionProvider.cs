using Microsoft.Data.SqlClient;
using System.Data;

namespace NCourseWork.Persistence.Common.Database
{
    internal class SqlConnectionProvider : IDbConnectionProvider
    {
        private readonly string connectionString;

        public SqlConnectionProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Task<IDbConnection> GetConnectionAsync()
        {
            return Task.FromResult((IDbConnection)new SqlConnection(connectionString));
        }
    }
}
