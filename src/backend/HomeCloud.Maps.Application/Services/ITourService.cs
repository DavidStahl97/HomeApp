using HomeCloud.Maps.Application.Dto;
using HomeCloud.Maps.Application.Dto.Tours;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Application.Services
{
    public interface ITourService
    {
        Task<IEnumerable<RouteDto>> GetAllRoutes(string userId);
        Task<IEnumerable<TourInfoDto>> GetAllTourInfosAsync(string userId);
        Task<TourDto> GetTourAsync(string tourId, string userId);
        Task InsertToursFromKomoot(string userId, KomootToursRequest request);
    }
}