using HomeCloud.Maps.Shared;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Application.Commands
{
    public interface IStoreKomootTour
    {
        Task ExecuteAsync(string userId, KomootToursRequest request);
    }
}