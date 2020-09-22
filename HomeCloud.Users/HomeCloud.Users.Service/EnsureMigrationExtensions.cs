using HomeCloud.User.Server;
using IdentityServer4.Test;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HomeCloud.Users.Service
{
    public static class EnsureMigrationExtensions
    {
        public static IHost EnsureMigration<T>(this IHost host)
            where T : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                Migrate<T>(scope);
            }

            return host;
        }

        private static void Migrate<T>(IServiceScope scope, int attempts = 5)
            where T : DbContext
        {
            try
            {
                var context = scope.ServiceProvider.GetService<T>();
                context.Database.Migrate();
            }
            catch (SqlException ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, $"Connection error while trying to migrate. Attempts: { attempts }");
                
                if (attempts == 0)
                {
                    throw ex;
                }
                else
                {
                    Thread.Sleep(10000);
                    Migrate<T>(scope, attempts--);
                }
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while migrating the database.");
                throw ex;
            }
        }
    }
}
