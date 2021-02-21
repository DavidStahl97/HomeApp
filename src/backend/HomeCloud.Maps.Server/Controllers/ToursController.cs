using HomeCloud.Maps.Application.Dto;
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
    public class ToursController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ToursController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{tourId}", Name = nameof(GetTourInfosById))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TourDto>> GetTourInfosById(string tourId)
        {
            var jwt = HttpContext.GetJsonWebToken();

            var request = new GetTourInfosByIdRequest
            {
                UserId = jwt.Subject,
                TourId = tourId
            };

            var result = await _mediator.Send(request);
            return result.Match<ActionResult<TourDto>>(
                x => Ok(x),
                x => NotFound());
        }

        [HttpPost(Name = nameof(PostTours))]
        public async Task<IActionResult> PostTours([FromBody] KomootToursRequest request)
        {
            var jwt = HttpContext.GetJsonWebToken();

            var insertRequest = new InsertToursFromKomootRequest
            {
                UserId = jwt.Subject,
                KomootToursRequest = request
            };

            var result = await _mediator.Send(insertRequest);

            return result.Match<IActionResult>(
                succesful => Ok(),
                settingsNotFound => NotFound());
        }
    }
}
