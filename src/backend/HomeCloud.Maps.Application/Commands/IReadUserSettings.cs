using HomeCloud.Maps.Shared;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Application.Commands
{
    public interface IReadUserSettings
    {
        Task<UserSettingsRequest> ExecuteAsync(string userId);
    }
}