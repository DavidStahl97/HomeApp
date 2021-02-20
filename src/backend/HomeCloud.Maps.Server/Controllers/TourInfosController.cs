using HomeCloud.Maps.Application.Dto;
using HomeCloud.Maps.Application.Dto.Tours;
using HomeCloud.Maps.Application.Handlers;
using HomeCloud.Maps.Application.Handlers.Tours;
using HomeCloud.Maps.Domain;
using HomeCloud.Maps.Domain.Types;
using HomeCloud.Maps.Server.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<TourInfosController> _logger;

        public TourInfosController(IMediator mediator, ILogger<TourInfosController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet(Name = nameof(GetTourInfosPagination))]
        public async Task<PaginationResult<TourInfoDto>> GetTourInfosPagination(int pageSize = 10, int pageIndex = 0, string tourNameFilter = "") 
        {
            var jwt = HttpContext.GetJsonWebToken();

            _logger.WriteInformation("Get Tour Info Page",
                (nameof(pageSize), pageSize),
                (nameof(pageIndex), pageIndex),
                (nameof(tourNameFilter), tourNameFilter),
                (nameof(jwt.Subject), jwt.Subject));

            MaybeNull<string> filter = string.IsNullOrEmpty(tourNameFilter) ? new Null() : tourNameFilter;

            var request = new GetTourInfosPaginationRequest
            {
                UserId = jwt.Subject,
                TourNameFilter = filter,
                Page = new Page { Index = pageIndex, Size = pageSize }
            };

            var result = await _mediator.Send(request);

            _logger.WriteInformation("Get Tour Info Page Result",
                (nameof(result.Total), result.Total));

            return result;
        }
    }
}
