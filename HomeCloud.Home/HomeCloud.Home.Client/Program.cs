using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using HomeCloud.Client.Common.Services;
using HomeCloud.Home.Client.Services;
using HomeCloud.Home.Client.Services.Authentication;

namespace HomeCloud.Home.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddOidcAuthentication(options =>
            {
                // Configure your authentication provider options here.
                // For more information, see https://aka.ms/blazor-standalone-auth
                options.ProviderOptions.Authority = builder.Configuration["Google:Authority"];
                options.ProviderOptions.ClientId = builder.Configuration["Google:ClientId"];
                options.ProviderOptions.ResponseType = builder.Configuration["Google:ResponseType"];

                var scopes = options.ProviderOptions.DefaultScopes;
                foreach (var scope in builder.Configuration["Google:Scopes"].Split(";"))
                {
                    scopes.Add(scope);
                }

                options.UserOptions.AuthenticationType = builder.Configuration["Google:AuthenticationType"];
            });

            ConfigureServices(builder.Services);

            builder.Services.AddAuthorizationCore();

            await builder.Build().RunAsync();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<IStorage, Storage>();
            services.AddScoped<IJwtStorage, JwtStorage>();       
            
            foreach (var app in Apps.AppStartups) 
            {
                app.ConfigureServices(services);
            }
        }
    }
}
