using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Domain.User
{
    public interface IUserService
    {
        public Task<User?> Login(string username, string password);
        public User? CurrentUser { get; }
        public Task Logout();
    }
}
