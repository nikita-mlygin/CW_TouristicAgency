namespace NCourseWork.Infrastructure.User
{
    using NCourseWork.Domain.User;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public User? CurrentUser { get; private set; }

        public async Task<User?> Login(string username, string password)
        {
            if (CurrentUser is not null)
            {
                CurrentUser = null;
            }

            CurrentUser = await userRepository.GetByLoginAndPassword(username, password);

            return CurrentUser;
        }

        public Task Logout()
        {
            CurrentUser = null;

            return Task.CompletedTask;
        }
    }
}
