using HomeCloud.Maps.Application.Database;
using HomeCloud.Maps.Application.Dto;
using HomeCloud.Maps.Application.Komoot;
using HomeCloud.Maps.Domain.Settings;
using HomeCloud.Maps.Domain.Tours;
using HomeCloud.Maps.Domain.Types;
using MediatR;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Application.Handlers.Tours
{
    public class InsertToursFromKomootRequest : IRequest<InsertToursFromKomootResponse>
    {
        public string UserId { get; init; }

        public KomootToursRequest KomootToursRequest { get; init; }
    }

    public struct SettingsNotFound { }

    public class InsertToursFromKomootResponse : OneOfBase<Task<Successful>, Task<SettingsNotFound>>
    {
        protected InsertToursFromKomootResponse(OneOf<Task<Successful>, Task<SettingsNotFound>> input)
            : base(input)
        {
        }

        public static implicit operator InsertToursFromKomootResponse(Task<Successful> _) => new InsertToursFromKomootResponse(_);
        public static implicit operator InsertToursFromKomootResponse(Task<SettingsNotFound> _) => new InsertToursFromKomootResponse(_);
    }

    public class InsertToursFromKomootHandler : IRequestHandler<InsertToursFromKomootRequest, InsertToursFromKomootResponse>
    {
        private readonly IRepository _repository;
        private readonly IKomootService _komootService;

        public InsertToursFromKomootHandler(IRepository repository, IKomootService komootService)
        {
            _repository = repository;
            _komootService = komootService;
        }

        public async Task<InsertToursFromKomootResponse> Handle(InsertToursFromKomootRequest request, CancellationToken cancellationToken)
        {
            var settings = await _repository.UserSettingsCollection
                .FirstAsync(request.UserId);

            return settings.Match<InsertToursFromKomootResponse>(
                x => StoreAsync(x, request),
                x => Task.FromResult(new SettingsNotFound()));
        }

        private async Task<Successful> StoreAsync(Domain.Settings.UserSettings settings, 
            InsertToursFromKomootRequest request)
        {
            var tours = await _komootService.GetAllTours(settings.KomootUserId, 
                request.KomootToursRequest.Cookies);

            await AddTourInfosAsync(settings.UserId, tours);
            await AddRoutesAsync(settings.UserId, tours);

            return new Successful();
        }

        private async Task AddTourInfosAsync(string userId, IEnumerable<Tour> tours)
        {
            var tourInfo = tours.Select(x => x.Info).ToList();
            tourInfo.ForEach(x => x.UserId = userId);

            await _repository.TourInfoCollection.InsertManyAsync(tourInfo);
        }

        private async Task AddRoutesAsync(string userId, IEnumerable<Tour> tours)
        {
            var routes = tours.Select(x => x.Route).ToList();
            routes.ForEach(x => x.UserId = userId);

            await _repository.RouteCollection.InsertManyAsync(routes);
        }
    }
}
