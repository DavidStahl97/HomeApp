using HomeCloud.Maps.Application.Dto;
using HomeCloud.Maps.Application.Services;
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
        private readonly ILogger<UserSettingsController> _logger;
        private readonly IUserSettingsService _userSettingsService;

        public UserSettingsController(ILogger<UserSettingsController> logger, IUserSettingsService userSettingsService)
        {
            _logger = logger;
            _userSettingsService = userSettingsService;
        }

        [HttpPost(Name = nameof(PostUserSettings))]
        public async Task PostUserSettings([FromBody] UserSettingsDto body)
        {
            _logger.LogInformation($"Post UserSettings { body.KomootUserId }");
            var userId = HttpContext.User.Claims.Single(x => x.Type == "sub").Value;
            await _userSettingsService.InsertUserSettingsAsync(body, userId);
        }

        [HttpGet(Name = nameof(GetUserSettings))]
        public async Task<UserSettingsDto> GetUserSettings()
        {
            var userId = HttpContext.User.Claims.Single(x => x.Type == "sub").Value;
            return await _userSettingsService.GetUserSettingsAsync(userId);
        }
    }
}
