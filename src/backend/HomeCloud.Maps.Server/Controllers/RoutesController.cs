using HomeCloud.Maps.Application.Dto.Tours;
using HomeCloud.Maps.Application.Services;
using HomeCloud.Maps.Server.Extensions;
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
        private readonly ITourService _tourService;

        public RoutesController(ITourService tourService)
        {
            _tourService = tourService;
        }

        [HttpGet(Name = nameof(GetAllRoutes))]
        public async Task<IEnumerable<RouteDto>> GetAllRoutes()
        {
            var jwt = HttpContext.GetJsonWebToken();
            return await _tourService.GetAllRoutes(jwt.Subject);
        }
    }
}
