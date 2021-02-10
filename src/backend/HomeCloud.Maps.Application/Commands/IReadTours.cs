using HomeCloud.Maps.Shared.Tours;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Application.Commands
{
    public interface IReadTours
    {
        Task<IEnumerable<TourInfoDto>> ExecuteAsync(string userId);
    }
}