﻿using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Server.Configure
{
    public static class LoggerExtensions
    {
        public static void AddLogging(string logConnectionString, bool inProduction)
        {
            var client = new MongoClient(logConnectionString);
            var db = client.GetDatabase("logs");

            var eventLevel = inProduction ? LogEventLevel.Information : LogEventLevel.Debug;

            var configuration = new LoggerConfiguration()
              .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
              .Enrich.FromLogContext()
              .WriteTo.MongoDB(db,
                eventLevel,
                "homecloud-maps-logs",
                1,
                TimeSpan.FromSeconds(1));

            if (inProduction == false)
            {
                configuration = configuration
                    .WriteTo.Debug()
                    .WriteTo.Console();
            }

            var logger = configuration.CreateLogger();

            Log.Logger = logger;

            if (inProduction == false)
            {
                Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
            }
        }
    }
}
