using HomeCloud.Maps.Domain;
using HomeCloud.Maps.Application.Database;
using HomeCloud.Maps.Application.Dto;
using HomeCloud.Maps.Application.Dto.Tours;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HomeCloud.Maps.Domain.Types;
using AutoMapper;
using System.Collections.Generic;

namespace HomeCloud.Maps.Application.Handlers.Tours
{
    public class GetTourInfosPaginationRequest : IRequest<PaginationResult<TourInfoDto>>
    {
        public string UserId { get; init; }

        public MaybeNull<string> TourNameFilter { get; init; }

        public Page Page { get; init; }
    }

    public class GetTourInfoPaginationHandler : IRequestHandler<GetTourInfosPaginationRequest, PaginationResult<TourInfoDto>>
    {
        private readonly IRepository _repository;
        private readonly ILogger<GetTourInfosPaginationRequest> _logger;
        private readonly IMapper _mapper;

        public GetTourInfoPaginationHandler(IRepository repository, ILogger<GetTourInfosPaginationRequest> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PaginationResult<TourInfoDto>> Handle(GetTourInfosPaginationRequest request, CancellationToken cancellationToken)
        {
            _logger.WriteInformation("Find Tour Page",
                (nameof(request.UserId), request.UserId),
                (nameof(request.TourNameFilter), request.TourNameFilter.Value),
                (nameof(request.Page.Index), request.Page.Index),
                (nameof(request.Page.Size), request.Page.Size));
            
            var result = await _repository.TourInfoCollection
                .FindPageAsync(request.UserId, request.Page.Index, request.Page.Size, request.TourNameFilter);

            var resultPageSize = result.Page.Count();

            _logger.WriteInformation("Find Tour Page Result",
                (nameof(result.Count), result.Count),
                (nameof(resultPageSize), resultPageSize));

            var tours = _mapper.Map<IEnumerable<TourInfoDto>>(result.Page);

            return new PaginationResult<TourInfoDto>
            {
                Data = tours,
                Total = (int)result.Count
            };
        }
    }
}
