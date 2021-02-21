using AutoMapper;
using HomeCloud.Maps.Application.Database;
using HomeCloud.Maps.Application.Dto.Tours;
using HomeCloud.Maps.Domain.Tours;
using HomeCloud.Maps.Domain.Types;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Application.Handlers.Tours
{
    public class GetTourInfosByIdRequest : IRequest<MaybeNull<TourDto>>
    {
        public string UserId { get; init; }

        public string TourId { get; init; }
    }

    public class GetTourByIdHandler : IRequestHandler<GetTourInfosByIdRequest, MaybeNull<TourDto>>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public GetTourByIdHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<MaybeNull<TourDto>> Handle(GetTourInfosByIdRequest request, CancellationToken cancellationToken)
        {
            var tourInfoTask = _repository.TourInfoCollection
                .FirstAsync(request.UserId, request.TourId);

            var routeTask = _repository.RouteCollection
                .FirstAsync(request.UserId, request.TourId);

            await Task.WhenAll(tourInfoTask, routeTask);

            return MaybeNull.Merge<TourInfo, Route>(tourInfoTask.Result, routeTask.Result)
                .Match<TourDto>(result => new TourDto
                {
                    TourInfo = _mapper.Map<TourInfoDto>(result.X),
                    Route = _mapper.Map<RouteDto>(result.Y),
                });                    
        }
    }
}
