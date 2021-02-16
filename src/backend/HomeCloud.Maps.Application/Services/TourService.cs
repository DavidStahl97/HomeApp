using HomeCloud.Maps.Application.Database;
using HomeCloud.Maps.Application.Dto;
using HomeCloud.Maps.Application.Dto.Tours;
using HomeCloud.Maps.Application.Komoot;
using HomeCloud.Maps.Domain.Tours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Application.Services
{
    class TourService : ITourService
    {
        private readonly IRepository _repository;
        private readonly IKomootService _komootService;

        public TourService(IRepository repository, IKomootService komootService)
        {
            _repository = repository;
            _komootService = komootService;
        }

        public async Task<TourDto> GetTourAsync(string tourId, string userId)
        {
            var tourInfo = await _repository.TourInfoCollection.SingleAsync(x => x.TourId == tourId && x.UserId == userId);
            var route = await _repository.RouteCollection.SingleAsync(x => x.TourId == tourId);

            return new TourDto
            {
                TourInfo = new TourInfoDto
                {
                    TourId = tourId,
                    Date = tourInfo.Date,
                    Distance = tourInfo.Distance,
                    Name = tourInfo.Name,
                    ImageUrl = tourInfo.ImageUrl
                },
                Route = new RouteDto
                {
                    Positions = route.Positions.Select(x => new PositionDto
                    {
                        Latitude = x.Latitude,
                        Longitude = x.Longitude
                    }).ToList()
                }
            };
        }

        public async Task<IEnumerable<TourInfoDto>> GetAllTourInfosAsync(string userId)
        {
            var tours = await _repository.TourInfoCollection.FindAsync(x => x.UserId == userId);

            return tours.Select(x => new TourInfoDto
            {
                TourId = x.TourId,
                Date = x.Date,
                Name = x.Name,
                Distance = x.Distance,
                ImageUrl = x.ImageUrl,
            }).ToList();
        }

        public async Task<IEnumerable<RouteDto>> GetAllRoutes(string userId)
        {
            var routes = await _repository.RouteCollection.FindAsync(x => x.UserId == userId);

            var dtos = routes.Select(x => new RouteDto
            {
                Positions = x.Positions.Select(x => new PositionDto
                {
                    Latitude = x.Latitude,
                    Longitude = x.Longitude
                }).ToList()
            }).ToList();

            return dtos;
        }

        public async Task InsertToursFromKomoot(string userId, KomootToursRequest request)
        {
            var settings = await _repository.UserSettingsCollection.SingleAsync(x => x.UserId == userId);

            var tours = await _komootService.GetAllTours(settings.KomootUserId, request.Cookies);

            await AddTourInfos(userId, tours);
            await AddRoutes(userId, tours);
        }

        private async Task AddTourInfos(string userId, IEnumerable<Tour> tours)
        {
            var tourInfo = tours.Select(x => x.Info).ToList();
            tourInfo.ForEach(x => x.UserId = userId);

            await _repository.TourInfoCollection.InsertManyAsync(tourInfo);
        }

        private async Task AddRoutes(string userId, IEnumerable<Tour> tours)
        {
            var routes = tours.Select(x => x.Route).ToList();
            routes.ForEach(x => x.UserId = userId);

            await _repository.RouteCollection.InsertManyAsync(routes);
        }
    }
}
