using HomeCloud.Maps.Application.Commands;
using HomeCloud.Maps.Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly IReadUserSettings _readUserSettings;
        private readonly ILogger<UserSettingsController> _logger;

        public UserSettingsController(IInsertUserSettings insertUserSettings, IReadUserSettings readUserSettings, ILogger<UserSettingsController> logger)
        {
            _insertUserSettings = insertUserSettings;
            _readUserSettings = readUserSettings;
            _logger = logger;
        }

        [HttpPost(Name = nameof(PostUserSettings))]
        public async Task PostUserSettings([FromBody] UserSettingsDto body)
        {
            _logger.LogInformation($"Post UserSettings { body.KomootUserId }");
            var userId = HttpContext.User.Claims.Single(x => x.Type == "sub").Value;
            await _insertUserSettings.ExecuteAsync(body, userId);
        }

        [HttpGet(Name = nameof(GetUserSettings))]
        public async Task<UserSettingsDto> GetUserSettings()
        {
            var userId = HttpContext.User.Claims.Single(x => x.Type == "sub").Value;
            return await _readUserSettings.ExecuteAsync(userId);
        }
    }
}
