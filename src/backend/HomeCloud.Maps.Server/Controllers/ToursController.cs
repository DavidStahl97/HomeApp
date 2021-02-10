using HomeCloud.Maps.Application.Commands;
using HomeCloud.Maps.Shared.Tours;
using Microsoft.AspNetCore.Authorization;
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
    public class ToursController : ControllerBase
    {

        [HttpGet]
        public Task<IEnumerable<TourInfoDto>> Get([FromServices] IReadTours service) 
        {
            // To-Do
            var userId = HttpContext.User.Claims.Single(x => x.Type == "sub").Value;

            return service.ExecuteAsync(userId);
        }
    }
}
