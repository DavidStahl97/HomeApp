using HomeCloud.Maps.Application.Database;
using HomeCloud.Maps.Application.Dto;
using HomeCloud.Maps.Domain.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Application.Services
{
    class UserSettingsService : IUserSettingsService
    {
        private readonly IRepository _repository;

        public UserSettingsService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserSettingsDto> GetUserSettingsAsync(string userId)
        {
            var settings = await _repository.UserSettingsCollection.SingleAsync(x => x.UserId == userId);

            if (settings == null)
            {
                return new UserSettingsDto { KomootUserId = string.Empty };
            }

            return new UserSettingsDto
            {
                KomootUserId = settings.KomootUserId
            };
        }

        public async Task InsertUserSettingsAsync(UserSettingsDto request, string userId)
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
