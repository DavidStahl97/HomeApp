using HomeCloud.Maps.Application.Commands;
using HomeCloud.Maps.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserSettingsController : ControllerBase
    {
        private readonly IInsertUserSettings _insertUserSettings;

        public UserSettingsController(IInsertUserSettings insertUserSettings)
        {
            _insertUserSettings = insertUserSettings;
        }

        [HttpPost]
        public async Task Post([FromBody] UserSettingsRequest body)
        {
            var userId = HttpContext.User.Claims.Single(x => x.Type == "sub").Value;
            await _insertUserSettings.ExecuteAsync(body, userId);
        }
    }
}
