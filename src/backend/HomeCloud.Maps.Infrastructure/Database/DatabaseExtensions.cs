using HomeCloud.Maps.Application.Database;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeCloud.Maps.Infrastructure.Database
{
    public static class DatabaseExtensions
    {
        public static void AddDatabase(this IServiceCollection services, DatabaseSettings settings)
        {
            var mongoClient = new MongoClient(settings.ConnectionString);
            services.AddSingleton(mongoClient);

            services.AddScoped<IRepository, Repository>();
        }
    }
}
