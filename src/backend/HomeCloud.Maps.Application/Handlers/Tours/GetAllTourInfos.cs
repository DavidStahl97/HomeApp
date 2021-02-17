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
    public class GetAllTourInfosRequest : IRequest<IEnumerable<TourInfoDto>>
    {
        public string UserId { get; init; }
    }

    public class GetAllTourInfosHandler : IRequestHandler<GetAllTourInfosRequest, IEnumerable<TourInfoDto>>
    {
        private readonly IRepository _repository;

        public GetAllTourInfosHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TourInfoDto>> Handle(GetAllTourInfosRequest request, CancellationToken cancellationToken)
        {
            var tours = await _repository.TourInfoCollection
                .FindAsync(x => x.UserId == request.UserId);

            return tours.Select(x => new TourInfoDto
            {
                TourId = x.TourId,
                Date = x.Date,
                Name = x.Name,
                Distance = x.Distance,
                ImageUrl = x.ImageUrl,
            }).ToList();
        }
    }
}
