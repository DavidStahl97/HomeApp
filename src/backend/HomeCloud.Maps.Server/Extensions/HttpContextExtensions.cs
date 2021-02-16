using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Server.Extensions
{
    record JsonWebToken(string Subject);

    static class HttpContextExtensions
    {
        public static JsonWebToken GetJsonWebToken(this HttpContext httpContext)
        {
            var subject = httpContext.User.Claims.Single(x => x.Type == "sub").Value;

            return new JsonWebToken(subject);
        }
    }
}
