using HomeCloud.Maps.Application.Database.Collections;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Infrastructure.Database.Collection
{
    abstract class CollectionBase<T> : ICollectionBase<T>
    {
        private const string _database = "homecloud-maps";
        private readonly MongoClient _client;

        public CollectionBase(MongoClient client)
        {
            _client = client;
        }

        public Task InsertAsync(T document)
        {
            return GetCollection().InsertOneAsync(document);                
        }

        public Task InsertManyAsync(IEnumerable<T> documents)
        {
            return GetCollection().InsertManyAsync(documents);
        }

        public async Task<T> SingleAsync(Expression<Func<T, bool>> expression)
        {
            var result = await GetCollection().Find(expression).SingleOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await GetCollection().Find(expression).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression, int index, int pageSize)
        {
            return await GetCollection()
                .Find(expression)
                .Skip(pageSize * index)
                .Limit(pageSize)
                .ToListAsync();
        }

        public Task ReplaceOrInsert(Expression<Func<T, bool>> expression, T document)
        {
            return GetCollection().ReplaceOneAsync(expression, document, new ReplaceOptions
            {
                IsUpsert = true
            });
        }

        protected IMongoCollection<T> GetCollection()
            => _client.GetDatabase(_database).GetCollection<T>(typeof(T).Name);
    }
}
