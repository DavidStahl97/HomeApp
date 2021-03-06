using HomeCloud.Maps.Application;
using HomeCloud.Maps.Application.Komoot;
using HomeCloud.Maps.Infrastructure;
using HomeCloud.Maps.Infrastructure.Database;
using HomeCloud.Maps.Infrastructure.GPX.Model;
using HomeCloud.Maps.Infrastructure.Komoot;
using HomeCloud.Maps.Server.Configure;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Linq;

namespace HomeCloud.Maps.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwt => jwt.UseGoogle(
                    clientId: Configuration["Google:ClientId"]
                ));

            services.AddDatabase(new DatabaseSettings
            {
                ConnectionString = Configuration["MongoDb:ConnectionString"]
            });

            services.AddSwaggerDocumentation();

            services.AddMediatR(Application.Application.GetAssembly());

            services.AddAutoMapper(Application.Application.GetAssembly());

            services.AddInfrastructureServices();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HomeCloud.Maps V1");
            });

            app.UseSerilogRequestLogging();

            app.AddApiRouting();
            app.AddBlazorRouting();
        }
    }
}
