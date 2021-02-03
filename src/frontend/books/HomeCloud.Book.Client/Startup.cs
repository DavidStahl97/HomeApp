using HomeCloud.Client.Common;
using HomeCloud.Client.Common.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeCloud.Book.Client
{
    public class Startup : IStartup
    {
        public App App => new App
        {
            Name = "Books",
            Description = "Search Books and get suggestions",
            AppLink = "books",
            NavigationItems = new List<NavigationItem>
            {
                new NavigationItem { Icon = "oi-plus", Link = "counter", Title = "Counter" },
            }
        };

        public void ConfigureServices(IServiceCollection services)
        {
            
        }
    }
}
