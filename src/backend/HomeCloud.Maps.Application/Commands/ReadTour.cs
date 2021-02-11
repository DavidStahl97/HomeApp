using HomeCloud.Maps.Application.Database;
using HomeCloud.Maps.Shared.Tours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Application.Commands
{
    public class ReadTour : IReadTour
    {
        private readonly IRepository _repository;

        public ReadTour(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<TourDto> ExecuteAsync(string tourId)
        {
            var tourInfo = await _repository.TourInfoCollection.SingleAsync(x => x.TourId == tourId);
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
    }
}
