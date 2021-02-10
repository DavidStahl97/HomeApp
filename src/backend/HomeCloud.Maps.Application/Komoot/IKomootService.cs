using HomeCloud.Maps.Domain.Tours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Application.Komoot
{
    public interface IKomootService
    {
        Task<IEnumerable<Tour>> GetAllTours(string userId, string cookies);
    }
}
