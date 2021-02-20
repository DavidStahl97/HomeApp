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
        public Task<TourDto> GetTourInfosById(string tourId)
        {
            var jwt = HttpContext.GetJsonWebToken();

            var request = new GetTourInfosByIdRequest
            {
                UserId = jwt.Subject,
                TourId = tourId
            };

            return _mediator.Send(request);
        }

        [HttpPost(Name = nameof(PostTours))]
        public Task PostTours([FromBody] KomootToursRequest request)
        {
            var jwt = HttpContext.GetJsonWebToken();

            var insertRequest = new InsertToursFromKomootRequest
            {
                UserId = jwt.Subject,
                KomootToursRequest = request
            };

            return _mediator.Send(insertRequest);
        }
    }
}
