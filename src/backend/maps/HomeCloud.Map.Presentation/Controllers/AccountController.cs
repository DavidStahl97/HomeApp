using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HomeCloud.Application.Identity;
using HomeCloud.Shared;
using HomeCloud.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HomeCloud.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateUser([FromBody] UserInfo model)
        {
            var result = await _accountService.CreateUser(model);

            return result.Match<IActionResult>(
                token => Ok(token),
                invalid => BadRequest("Registration invalid"));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserInfo userInfo)
        {
            var result = await _accountService.Login(userInfo);

            return result.Match<IActionResult>(
                token => Ok(token),
                invalid => BadRequest("Username or password invalid"));            
        }
    }
}
