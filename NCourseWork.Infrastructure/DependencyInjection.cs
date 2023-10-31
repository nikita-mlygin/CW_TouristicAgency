using Microsoft.Extensions.DependencyInjection;
using NCourseWork.Domain.Client;
using NCourseWork.Domain.Purchase;
using NCourseWork.Domain.User;
using NCourseWork.Infrastructure.Client;
using NCourseWork.Infrastructure.Purchase;
using NCourseWork.Infrastructure.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IPurchaseService, PurchaseService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IClientService, ClientService>(); 

            return services;
        }
    }
}
