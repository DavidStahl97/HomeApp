using HomeCloud.Maps.Application.Dto.Tours;
using HomeCloud.Maps.Application.Handlers.Tours;
using HomeCloud.Maps.Server.Extensions;
using MediatR;
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
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class RoutesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoutesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = nameof(GetAllRoutes))]
        public Task<IEnumerable<RouteDto>> GetAllRoutes()
        {
            var jwt = HttpContext.GetJsonWebToken();

            var request = new GetAllRoutesRequest
            {
                UserId = jwt.Subject
            };

            return _mediator.Send(request);
        }
    }
}
