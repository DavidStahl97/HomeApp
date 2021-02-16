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
    public class TourInfosController : ControllerBase
    {
        private readonly ITourService _tourService;

        public TourInfosController(ITourService tourService)
        {
            _tourService = tourService;
        }

        [HttpGet(Name = nameof(GetAllTourInfos))]
        public Task<IEnumerable<TourInfoDto>> GetAllTourInfos() 
        {
            var jwt = HttpContext.GetJsonWebToken();
            return _tourService.GetAllTourInfosAsync(jwt.Subject);
        }
    }
}
