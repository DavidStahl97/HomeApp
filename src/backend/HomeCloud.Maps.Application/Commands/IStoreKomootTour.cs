using HomeCloud.Maps.Application.Dto;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Application.Commands
{
    public interface IStoreKomootTour
    {
        Task ExecuteAsync(string userId, KomootToursRequest request);
    }
}