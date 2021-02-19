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
    public class GetTourInfosByIdRequest : IRequest<TourDto>
    {
        public string UserId { get; init; }

        public string TourId { get; init; }
    }

    public class GetTourByIdHandler : IRequestHandler<GetTourInfosByIdRequest, TourDto>
    {
        private readonly IRepository _repository;

        public GetTourByIdHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<TourDto> Handle(GetTourInfosByIdRequest request, CancellationToken cancellationToken)
        {
            var tourInfo = await _repository.TourInfoCollection
                .FirstAsync(x => x.TourId == request.TourId && x.UserId == request.UserId);

            var route = await _repository.RouteCollection
                .FirstAsync(x => x.TourId == request.TourId && x.UserId == request.UserId);

            return new TourDto
            {
                TourInfo = new TourInfoDto
                {
                    TourId = request.TourId,
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
