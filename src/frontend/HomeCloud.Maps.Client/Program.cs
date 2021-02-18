using HomeCloud.Maps.Client.Services;
using HomeCloud.Maps.Client.WebApi;
using HomeCloud.Maps.Client.WebApi.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });            

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

            builder.Services.AddAuthorizationCore();

            builder.Services.AddMudServices();

            AddServices(builder.Services);

            await builder.Build().RunAsync();
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IStorage, Storage>();
            services.AddScoped<IJwtStorage, JwtStorage>();
            services.AddScoped<IWebAPIClient, WebAPIClient>();
        }
    }
}
