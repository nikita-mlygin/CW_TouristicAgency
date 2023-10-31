using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Domain.User
{
    public interface IUserRepository
    {
        Task<User?> GetByLoginAndPassword(string login, string password);
    }
}
