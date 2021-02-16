using HomeCloud.Maps.Application.Commands;
using HomeCloud.Maps.Application.Dto;
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
    public class KomootController : ControllerBase
    {
        [HttpPatch(Name = nameof(PatchKomootRoutes))]
        public async Task<IActionResult> PatchKomootRoutes([FromBody] KomootToursRequest request, [FromServices] IStoreKomootTour service)
        {
            // To-Do
            var userId = HttpContext.User.Claims.Single(x => x.Type == "sub").Value;

            await service.ExecuteAsync(userId, request);

            return Ok();
        }
    }
}
