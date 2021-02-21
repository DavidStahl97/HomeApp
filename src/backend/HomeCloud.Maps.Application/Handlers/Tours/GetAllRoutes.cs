using HomeCloud.Maps.Application.Database;
using HomeCloud.Maps.Application.Dto.Tours;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Application.Handlers.Tours
{
    public class GetAllRoutesRequest : IRequest<IEnumerable<RouteDto>>
    {
        public string UserId { get; init; }
    }

    public class GetAllRoutesHandler : IRequestHandler<GetAllRoutesRequest, IEnumerable<RouteDto>>
    {
        private readonly IRepository _repository;

        public GetAllRoutesHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RouteDto>> Handle(GetAllRoutesRequest request, CancellationToken cancellationToken)
        {
            var routes = await _repository.RouteCollection
                .FindAsync(request.UserId);

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
