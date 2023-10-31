using Dapper;
using Microsoft.Extensions.DependencyInjection;
using NCourseWork.Domain.Client;
using NCourseWork.Domain.Country;
using NCourseWork.Domain.Hotel;
using NCourseWork.Domain.Purchase;
using NCourseWork.Domain.Status;
using NCourseWork.Domain.Tour;
using NCourseWork.Domain.User;
using NCourseWork.Persistence.Client;
using NCourseWork.Persistence.Common.Compiler;
using NCourseWork.Persistence.Common.Database;
using NCourseWork.Persistence.Common.TypeHandlers;
using NCourseWork.Persistence.Country;
using NCourseWork.Persistence.Hotel;
using NCourseWork.Persistence.Purchase;
using NCourseWork.Persistence.Status;
using NCourseWork.Persistence.Tour;
using NCourseWork.Persistence.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
        {
            SqlMapper.AddTypeHandler(new GuidTypeHandler());

            services.AddSingleton<IDbConnectionProvider, SqlConnectionProvider>(_ => new SqlConnectionProvider(connectionString));
            services.AddSingleton<ISqlCompilerProvider, SqlCompilerProvider>();

            services.AddSingleton<IClientRepository, ClientRepository>();
            services.AddSingleton<IStatusRepository, StatusRepository>();
            services.AddSingleton<ICountryRepository, CountryRepository>();
            services.AddSingleton<IHotelRepository, HotelRepository>();
            services.AddSingleton<ITourRepository, TourRepository>();
            services.AddSingleton<IPurchaseRepository, PurchaseRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();

            return services;
        }
    }
}
