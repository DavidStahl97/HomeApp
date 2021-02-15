using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Server.Configure
{
    public static class RoutingExtensions
    {
        public static void AddApiRouting(this IApplicationBuilder app)
        {
            app.MapWhen(context => IsApiResource(context), builder =>
            {
                builder.UseRouting();

                builder.UseAuthentication();
                builder.UseAuthorization();

                builder.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            });
        }

        public static void AddBlazorRouting(this IApplicationBuilder app)
        {
            app.MapWhen(context => IsBlazorResource(context), builder =>
            {
                builder.UseRouting();

                builder.UseEndpoints(endpoints =>
                {
                    endpoints.MapRazorPages();
                    endpoints.MapFallbackToFile("index.html");
                });
            });
        }

        private static bool IsApiResource(HttpContext context) => context.Request.Path.StartsWithSegments("/api");

        private static bool IsBlazorResource(HttpContext context) => IsApiResource(context) == false;

    }
}
