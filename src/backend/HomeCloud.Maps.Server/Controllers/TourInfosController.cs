using HomeCloud.Maps.Application.Dto;
using HomeCloud.Maps.Application.Dto.Tours;
using HomeCloud.Maps.Application.Handlers;
using HomeCloud.Maps.Application.Handlers.Tours;
using HomeCloud.Maps.Server.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static OneOf.Types.TrueFalseOrNull;

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

        [HttpGet(Name = nameof(GetTourInfosPagination))]
        public Task<PaginationResult<TourInfoDto>> GetTourInfosPagination(int pageSize = 10, int pageIndex = 0, string tourNameFilter = "") 
        {
            var jwt = HttpContext.GetJsonWebToken();

            OneOf<string, Null> filter = string.IsNullOrEmpty(tourNameFilter) ? new Null() : tourNameFilter;

            var request = new GetTourInfoPagination
            {
                UserId = jwt.Subject,
                TourNameFilter = filter,
                Page = new Page { Index = pageIndex, Size = pageSize }
            };

            return _mediator.Send(request);
        }
    }
}
