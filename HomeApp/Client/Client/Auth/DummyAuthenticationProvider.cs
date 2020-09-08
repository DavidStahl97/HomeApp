using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HomeApp.Client.Auth
{
    public class DummyAuthenticationProvider : AuthenticationStateProvider
    {
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var anonymous = new ClaimsIdentity(new List<Claim>
            {
                new Claim("key1", "value1"),
                new Claim(ClaimTypes.Name, "David"),
                new Claim(ClaimTypes.Role, "Admin")
            }, "sdsd");
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonymous)));
        }
    }
}
