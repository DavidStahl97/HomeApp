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
    public class TourInfosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TourInfosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = nameof(GetAllTourInfos))]
        public Task<IEnumerable<TourInfoDto>> GetAllTourInfos() 
        {
            var jwt = HttpContext.GetJsonWebToken();

            var request = new GetAllTourInfosRequest
            {
                UserId = jwt.Subject
            };

            return _mediator.Send(request);
        }
    }
}
