using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using NCourseWork.MVVM;
using NCourseWork.ViewModels.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services)
        {
            FindViewModelPageTypes(typeof(DependencyInjection).Assembly)
                .ToList()
                .ForEach(x =>
                {
                    if (typeof(ISinglePage).IsAssignableFrom(x))
                    {
                        services.AddSingleton(x);
                        return;
                    }

                    services.AddTransient(x);
                });

            return services;
        }

        public static IServiceCollection AddWindows(this IServiceCollection services)
        {
            services.AddSingleton<MainWindow>(services => new()
            {
                DataContext = services.GetRequiredService<MainViewModel>()
            });

            return services;
        }

        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;

            config.Scan(
                typeof(DependencyInjection).Assembly,
                typeof(Application.DependencyInjection).Assembly,
                typeof(Persistence.DependencyInjection).Assembly);

            services.AddSingleton(config);

            services.AddSingleton<IMapper, ServiceMapper>();

            return services;
        }

        private static IEnumerable<Type> FindViewModelPageTypes(Assembly assembly)
        {
            return assembly.GetTypes().Where(type => (typeof(IBasePage).IsAssignableFrom(type) || typeof(IBasePage<>).IsAssignableFrom(type)) && !type.IsInterface && !type.IsAbstract);
        }
    }
}
