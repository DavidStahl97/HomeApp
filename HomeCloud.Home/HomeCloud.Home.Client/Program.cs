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
                options.ProviderOptions.Authority = "https://accounts.google.com/";
                options.ProviderOptions.ClientId = "484017769198-51sqbl5eb8erjmnk4m9sk5vludjo24h6.apps.googleusercontent.com";
                options.ProviderOptions.ResponseType = "id_token";

                var scopes = options.ProviderOptions.DefaultScopes;
                scopes.Add("openid");
                scopes.Add("email");

                options.UserOptions.AuthenticationType = "google";
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
        }
    }
}
