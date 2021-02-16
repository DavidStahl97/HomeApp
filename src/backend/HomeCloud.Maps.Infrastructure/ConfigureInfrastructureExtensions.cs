using HomeCloud.Maps.Application.Komoot;
using HomeCloud.Maps.Infrastructure.GPX.Model;
using HomeCloud.Maps.Infrastructure.Komoot;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Infrastructure
{
    public static class ConfigureInfrastructureExtensions
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IKomootService, KomootService>();
            services.AddScoped<IGPXSerializer, GPXSerializer>();
        }
    }
}
