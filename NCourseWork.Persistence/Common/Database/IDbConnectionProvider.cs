using System.Data;

namespace NCourseWork.Persistence.Common.Database
{
    internal interface IDbConnectionProvider
    {
        public Task<IDbConnection> GetConnectionAsync();
    }
}
