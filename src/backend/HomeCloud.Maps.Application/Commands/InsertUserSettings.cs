using HomeCloud.Maps.Application.Database;
using HomeCloud.Maps.Application.Dto;
using HomeCloud.Maps.Domain.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Application.Commands
{
    public class InsertUserSettings : IInsertUserSettings
    {
        private readonly IRepository _repository;

        public InsertUserSettings(IRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(UserSettingsDto request, string userId)
        {
            var userSettings = new UserSettings
            {
                UserId = userId,
                KomootUserId = request.KomootUserId
            };

            await _repository.UserSettingsCollection.ReplaceOrInsert(x => x.UserId == userId, userSettings);
        }
    }
}
