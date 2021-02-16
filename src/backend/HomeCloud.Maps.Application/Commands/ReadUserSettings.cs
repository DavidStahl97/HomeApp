using HomeCloud.Maps.Application.Database;
using HomeCloud.Maps.Application.Dto;
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

        public async Task<UserSettingsDto> ExecuteAsync(string userId)
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
    }
}
