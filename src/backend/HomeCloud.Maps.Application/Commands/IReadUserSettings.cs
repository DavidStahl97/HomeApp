using HomeCloud.Maps.Application.Dto;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Application.Commands
{
    public interface IReadUserSettings
    {
        Task<UserSettingsDto> ExecuteAsync(string userId);
    }
}