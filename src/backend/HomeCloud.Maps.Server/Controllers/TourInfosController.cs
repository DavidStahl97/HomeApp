using HomeCloud.Maps.Application.Commands;
using HomeCloud.Maps.Application.Dto.Tours;
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

        [HttpGet(Name = nameof(GetAllTourInfos))]
        public Task<IEnumerable<TourInfoDto>> GetAllTourInfos([FromServices] IReadTours service) 
        {
            // To-Do
            var userId = HttpContext.User.Claims.Single(x => x.Type == "sub").Value;

            return service.ExecuteAsync(userId);
        }

        [HttpGet("{id}", Name = nameof(GetTourInfosById))]
        public async Task<TourDto> GetTourInfosById(string id, [FromServices] IReadTour service)
        {
            return await service.ExecuteAsync(id);
        }
    }
}
