using HomeCloud.Maps.Application.Database;
using HomeCloud.Maps.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Application.Commands
{
    public class ReadUserSettings : IReadUserSettings
    {
        private readonly IRepository _repository;

        public ReadUserSettings(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserSettingsRequest> ExecuteAsync(string userId)
        {
            var settings = await _repository.UserSettingsCollection.SingleAsync(x => x.UserId == userId);

            if (settings == null)
            {
                return new UserSettingsRequest { KomootUserId = string.Empty };
            }

            return new UserSettingsRequest
            {
                KomootUserId = settings.KomootUserId
            };
        }
    }
}
