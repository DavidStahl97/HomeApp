using HomeCloud.Maps.Domain.Tours;
using HomeCloud.Maps.Domain.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Application.Database.Collections
{
    public interface IRouteCollection : ICollectionBase<Route>
    {
        Task<MaybeFound<Route>> FirstAsync(string userId, string tourId);

        Task<IEnumerable<Route>> FindAsync(string userId);
    }
}
