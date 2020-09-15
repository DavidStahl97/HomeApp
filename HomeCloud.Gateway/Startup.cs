using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace HomeCloud.Gateway
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddControllersWithViews();
            services.AddRazorPages();             

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwt => jwt.UseGoogle(
                    clientId: "484017769198-51sqbl5eb8erjmnk4m9sk5vludjo24h6.apps.googleusercontent.com"
            ));

            services.AddOcelot();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }            

            app.UseHttpsRedirection();                        

            app.UseWhen(context => IsApiCall(context) == false,
                builder =>
                {
                    builder.UseBlazorFrameworkFiles();
                    builder.UseStaticFiles();

                    builder.UseRouting();

                    // Must be before UseEndPoints   
                    builder.UseAuthentication();
                    builder.UseAuthorization();

                    builder.UseEndpoints(endpoints =>
                    {
                        endpoints.MapRazorPages();
                        endpoints.MapFallbackToFile("index.html");
                    });
                });


            app.UseWhen(context => IsApiCall(context),
                async builder =>
                {
                    builder.UseRouting();

                    await app.UseOcelot();
                });            
        }

        private static bool IsApiCall(HttpContext context)
        {
            var path = context.Request.Path;
            if (path.HasValue == false)
            {
                return false;
            }

            bool isApiCall = path.Value.StartsWith("/api/");
            return isApiCall;
        }
    }
}
