using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCloud.Users.Service
{
    public static class EnsureMigrationExtensions
    {
        public static IHost EnsureMigration<T>(this IHost host)
            where T : DbContext
        {
            using (var scrope = host.Services.CreateScope())
            {
                var context = scrope.ServiceProvider.GetService<T>();
                context.Database.Migrate();
            }

            return host;
        }
    }
}
