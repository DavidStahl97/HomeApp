using HomeCloud.Maps.Application.Database;
using HomeCloud.Maps.Application.Dto.Tours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Application.Commands
{
    public class ReadAllRoutes : IReadAllRoutes
    {
        private readonly IRepository _repository;

        public ReadAllRoutes(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RouteDto>> ExecuteAsync(string userId)
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
    }
}
