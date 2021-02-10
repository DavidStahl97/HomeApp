using HomeCloud.Maps.Shared;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Application.Commands
{
    public interface IInsertUserSettings
    {
        Task ExecuteAsync(UserSettingsDto request, string userId);
    }
}