using HomeCloud.Maps.Application.Database.Collections;
using HomeCloud.Maps.Domain.Tours;
using HomeCloud.Maps.Infrastructure.Database.Collection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Infrastructure.Database.Collections
{
    public class RouteCollection : CollectionBase<Route>, IRouteCollection
    {
        public RouteCollection(MongoClient client) : base(client)
        {
        }

        public Task<IEnumerable<Route>> FindAsync(string userId)
            => FindAsync(x => x.UserId == userId);

        public Task<Route> FirstAsync(string userId, string tourId)
            => FirstAsync(x => x.TourId == tourId && x.UserId == userId);
    }
}
