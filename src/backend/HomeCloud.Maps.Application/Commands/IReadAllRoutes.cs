using HomeCloud.Maps.Application.Dto.Tours;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Application.Commands
{
    public interface IReadAllRoutes
    {
        Task<IEnumerable<RouteDto>> ExecuteAsync(string userId);
    }
}