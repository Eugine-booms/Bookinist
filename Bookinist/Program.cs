using MathCore.WPF;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bookinist
{

    public class Program
    {


        [STAThread]
        public static void Main(string [] args)
        {
            
            var app = new App();
            app.InitializeComponent();
            app.Run();
        }

      public   static IHostBuilder CreateHostBuilder(string[] args) => Host
        .CreateDefaultBuilder(args)
            .ConfigureServices(App.ConfigureServices)
            .ConfigureHostConfiguration(configHost =>
            {
                configHost.SetBasePath(Directory.GetCurrentDirectory());
                configHost.AddJsonFile("hostsettings.json", optional: true);
                configHost.AddEnvironmentVariables(prefix: "PREFIX_");
                configHost.AddCommandLine(args);

            });



    }
}
