using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Server
{
    public class Program
    {
        private const string PRODUCTION_CONTEXT = "Production";
        private static readonly string Context = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? PRODUCTION_CONTEXT;

        private static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Context}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        public static void Main(string[] args)
        {
            var logConnectionString = Configuration["MongoDb:ConnectionString"];
            var inProduction = Context == PRODUCTION_CONTEXT;
            LoggerExtensions.AddLogging(logConnectionString, inProduction);

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();
    }
}
