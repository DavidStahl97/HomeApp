using HomeCloud.Maps.Application.Database;
using HomeCloud.Maps.Application.Komoot;
using HomeCloud.Maps.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Application.Commands
{
    public class StoreKomootTour : IStoreKomootTour
    {
        private readonly IRepository _repository;
        private readonly IKomootService _komootService;

        public StoreKomootTour(IRepository repository, IKomootService komootService)
        {
            _repository = repository;
            _komootService = komootService;
        }

        public async Task ExecuteAsync(string userId, KomootToursRequest request)
        {
            var settings = await _repository.UserSettingsCollection.SingleAsync(x => x.UserId == userId);

            var tours = await _komootService.GetAllTours(settings.KomootUserId, request.Cookies);
            var tourInfo = tours.Select(x => x.Info).ToList();
            tourInfo.ForEach(x => x.UserId = userId);

            await _repository.TourInfoCollection.InsertManyAsync(tourInfo);
        }
    }
}
