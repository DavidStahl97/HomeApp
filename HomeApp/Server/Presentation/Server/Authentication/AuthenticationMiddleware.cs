using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HomeApp.Server.Authentication
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string authHeader = context.Request.Headers["Authorization"];

            if (authHeader is null == false)
            {
                await ReadJwt(authHeader, context);
            }

            await _next(context);
        }

        private static async Task ReadJwt(string authHeader, HttpContext context)
        {
            var split = authHeader.Split(" ");
            if (split.Length != 2)
            {
                return;
            }

            var bearerType = split[0];
            if (bearerType != "Bearer")
            {
                return;
            }

            var jwt = split[1];
            if (string.IsNullOrWhiteSpace(jwt))
            {
                return;
            }

            try
            {
                var validPayload = await GoogleJsonWebSignature.ValidateAsync(jwt);
                AttachUserInformation(validPayload, context);    
            }
            catch (InvalidJwtException)
            {
                return;
            }
        }

        private static void AttachUserInformation(GoogleJsonWebSignature.Payload payload, HttpContext context)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, payload.Name),
                new Claim(ClaimTypes.Email, payload.Email)
            };

            var identity = new ClaimsIdentity(claims, "basic");
            context.User = new ClaimsPrincipal(identity);
        }
    }
}
