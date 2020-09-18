using HomeCloud.Client.Common;
using HomeCloud.Client.Common.Model;
using HomeCloud.Map.Client.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeCloud.Map.Client
{
    public class Startup : IStartup
    {
        public App App => new App
        {
            Name = "Map",
            Description = "Inspect your Routes that your take",
            AppLink = "map",
            NavigationItems = new List<NavigationItem>
            {
                new NavigationItem { Icon = "oi-plus", Link = "counter", Title = "Counter" },
                new NavigationItem { Icon = "oi-list-rich", Link = "fetchdata", Title = "Fetch data" }
            }
        };

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IWeatherForcastRepository, WeatherForacastRepository>();
        }
    }
}
