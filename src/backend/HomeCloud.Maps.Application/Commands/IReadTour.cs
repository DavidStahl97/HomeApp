using HomeCloud.Maps.Shared.Tours;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Application.Commands
{
    public interface IReadTour
    {
        Task<TourDto> ExecuteAsync(string tourId);
    }
}