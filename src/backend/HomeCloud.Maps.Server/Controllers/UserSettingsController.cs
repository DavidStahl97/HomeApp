using HomeCloud.Maps.Application.Dto;
using HomeCloud.Maps.Application.Handlers.UserSettings;
using HomeCloud.Maps.Application.Services;
using HomeCloud.Maps.Server.Extensions;
using MediatR;
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
        private readonly IMediator _mediator;

        public UserSettingsController(ILogger<UserSettingsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost(Name = nameof(PostUserSettings))]
        public Task PostUserSettings([FromBody] UserSettingsDto body)
        {
            var jwt = HttpContext.GetJsonWebToken();

            var request = new UpdateUserSettingsRequest
            {
                UserId = jwt.Subject,
                UserSettings = body
            };

            return _mediator.Send(request);
        }

        [HttpGet(Name = nameof(GetUserSettings))]
        public Task<UserSettingsDto> GetUserSettings()
        {
            var jwt = HttpContext.GetJsonWebToken();

            var request = new GetUserSettingsRequest
            {
                UserId = jwt.Subject
            };

            return _mediator.Send(request);
        }
    }
}
