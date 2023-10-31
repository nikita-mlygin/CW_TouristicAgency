using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NCourseWork.Application;
using NCourseWork.Common.Layouts;
using NCourseWork.Infrastructure;
using NCourseWork.MVVM;
using NCourseWork.Persistence;
using NCourseWork.Services.Navigation;
using System;

namespace NCourseWork
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddPersistence("Data Source=DESKTOP-BF8P49A\\SQLEXPRESS;User ID=root;Password=qwert;Connect Timeout=30;Encrypt=False");
                    services.AddInfrastructure();
                    services.AddApplication();
                    services.AddViewModels();
                    services.AddMapper();
                    services.AddWindows();
                    services.AddSingleton<INavigationService, NavigationService>(
                        services => new NavigationService(t => (IBasePage)services.GetRequiredService(t)));
                    services.AddSingleton<App>();
                })
                .Build();

            var app = host.Services.GetService<App>();

            app?.Run();
        }
    }
}
