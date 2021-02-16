using HomeCloud.Maps.Application.Dto;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Application.Services
{
    public interface IUserSettingsService
    {
        Task<UserSettingsDto> GetUserSettingsAsync(string userId);

        Task InsertUserSettingsAsync(UserSettingsDto request, string userId);
    }
}