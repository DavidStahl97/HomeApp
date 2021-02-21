using HomeCloud.Maps.Application.Database.Collections;
using HomeCloud.Maps.Domain.Tours;
using HomeCloud.Maps.Domain.Types;
using HomeCloud.Maps.Infrastructure.Database.Collection;
using MongoDB.Bson;
using MongoDB.Driver;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OneOf.Types.TrueFalseOrNull;

namespace HomeCloud.Maps.Infrastructure.Database.Collections
{
    public class TourInfoCollection : CollectionBase<TourInfo>, ITourInfoCollection
    {
        public TourInfoCollection(MongoClient client) : base(client)
        {
        }

        public async Task<(IEnumerable<TourInfo> Page, long Count)> FindPageAsync(string userId, int index, int pageSize, MaybeNull<string> tourNameFilter)
        {
            var filter = Builders<TourInfo>.Filter.Eq(x => x.UserId, userId);

            filter = tourNameFilter.Match(
                x => filter & Builders<TourInfo>.Filter.Regex(x => x.Name, new BsonRegularExpression(x)),
                x => filter
            );

            var pageTask = GetCollection()
                .Find(filter)
                .Skip(pageSize * index)
                .Limit(pageSize)
                .ToListAsync();

            var countTask = GetCollection().CountDocumentsAsync(filter);

            await Task.WhenAll(pageTask, countTask);

            return (pageTask.Result, countTask.Result);
        }
    }
}
