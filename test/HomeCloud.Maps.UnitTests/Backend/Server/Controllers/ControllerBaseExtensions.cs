using HomeCloud.Maps.Server.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Maps.UnitTests.Backend.Server.Controllers
{
    public static class ControllerBaseExtensions
    {
        public static void AddJwt(this ControllerBase controller, JsonWebToken jwt)
        {
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim("sub", jwt.Subject),
                    }, "mock"))
                }
            };
        }
    }
}
