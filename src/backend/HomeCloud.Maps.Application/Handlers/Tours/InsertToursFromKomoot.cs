using HomeCloud.Maps.Application.Database;
using HomeCloud.Maps.Application.Dto;
using HomeCloud.Maps.Application.Komoot;
using HomeCloud.Maps.Domain.Tours;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Application.Handlers.Tours
{
    public class InsertToursFromKomootRequest : IRequest
    {
        public string UserId { get; init; }

        public KomootToursRequest KomootToursRequest { get; init; }
    }

    public class InsertToursFromKomootHandler : IRequestHandler<InsertToursFromKomootRequest>
    {
        private readonly IRepository _repository;
        private readonly IKomootService _komootService;

        public InsertToursFromKomootHandler(IRepository repository, IKomootService komootService)
        {
            _repository = repository;
            _komootService = komootService;
        }

        public async Task<Unit> Handle(InsertToursFromKomootRequest request, CancellationToken cancellationToken)
        {
            var settings = await _repository.UserSettingsCollection
                .FirstAsync(x => x.UserId == request.UserId);

            var tours = await _komootService.GetAllTours(settings.KomootUserId, request.KomootToursRequest.Cookies);

            await AddTourInfos(request.UserId, tours);
            await AddRoutes(request.UserId, tours);

            return Unit.Value;
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
