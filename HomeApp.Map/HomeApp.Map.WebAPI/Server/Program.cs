using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace HomeApp.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Infrastructure.Logging.Configuration.SetupLogging();
            CreateHost(args);
        }

        private static void CreateHost(string[] args)
        {            
            try
            {
                new Infrastructure.Logging.Logger().Information("Build and run host");                
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                new Infrastructure.Logging.Logger().Fatal($"Failed to start {Assembly.GetExecutingAssembly().GetName().Name}", ex);
                throw;
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration(configuration =>
                {
                    configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    configuration.AddJsonFile(
                        $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
                        optional: true);
                })
                .UseSerilog();
    }
}
