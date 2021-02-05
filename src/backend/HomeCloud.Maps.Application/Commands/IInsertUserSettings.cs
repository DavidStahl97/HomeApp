using HomeCloud.Maps.Shared;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Application.Commands
{
    public interface IInsertUserSettings
    {
        Task ExecuteAsync(UserSettingsRequest request, string userId);
    }
}