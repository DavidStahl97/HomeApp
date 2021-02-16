using HomeCloud.Maps.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Application
{
    public static class ConfigureApplicationExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITourService, TourService>();
            services.AddScoped<IUserSettingsService, UserSettingsService>();
        }
    }
}
