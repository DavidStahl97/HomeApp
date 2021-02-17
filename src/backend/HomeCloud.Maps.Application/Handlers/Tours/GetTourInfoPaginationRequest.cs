using HomeCloud.Maps.Application.Database;
using HomeCloud.Maps.Application.Dto;
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
    public class GetTourInfoPaginationRequest : IRequest<PaginationResult<TourInfoDto>>
    {
        public string UserId { get; init; }

        public Page Page { get; init; }
    }

    public class GetTourInfoPaginationHandler : IRequestHandler<GetTourInfoPaginationRequest, PaginationResult<TourInfoDto>>
    {
        private readonly IRepository _repository;

        public GetTourInfoPaginationHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginationResult<TourInfoDto>> Handle(GetTourInfoPaginationRequest request, CancellationToken cancellationToken)
        {
            var result = await _repository.TourInfoCollection
                .FindPageAsync(x => x.UserId == request.UserId, request.Page.Index, request.Page.Size);

            var tours = result.Page.Select(x => new TourInfoDto
            {
                TourId = x.TourId,
                Date = x.Date,
                Name = x.Name,
                Distance = x.Distance,
                ImageUrl = x.ImageUrl,
            }).ToList();

            return new PaginationResult<TourInfoDto>
            {
                Data = tours,
                Total = (int)result.Count
            };
        }
    }
}
