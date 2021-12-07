using Bookinist.DAL;
using Bookinist.Data;
using Bookinist.Services;
using Bookinist.ViewModels;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Bookinist
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IHost __Host;
        public static IHost Host => __Host
            ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();
        internal static void ConfigureServices(HostBuilderContext host, IServiceCollection services) =>
            services
            .RegisterServices()
            .RegisterViewModels()
            .RegisterDataBase(host.Configuration.GetSection("Database"))
            .RegisterRepositoryesInDB();
        public static IServiceProvider Services => Host.Services;
        protected async override void OnStartup(StartupEventArgs e)
        {
            var host = Host;
            using (var scope = Services.CreateScope())
            {
                 scope.ServiceProvider.GetRequiredService<DBInitializer>();  //
            }
            await host.StartAsync().ConfigureAwait(false);
            base.OnStartup(e);
        }
        protected async override void OnExit(ExitEventArgs e)
        {
            var host = Host;
            base.OnExit(e);
            await host.StopAsync().ConfigureAwait(false);
            host = null;
        }
    }
}
