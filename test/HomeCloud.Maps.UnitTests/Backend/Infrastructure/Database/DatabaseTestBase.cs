using AutoFixture;
using FluentAssertions;
using HomeCloud.Maps.Application.Database;
using HomeCloud.Maps.Application.Database.Collections;
using HomeCloud.Maps.Infrastructure.Database;
using Mongo2Go;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Maps.UnitTests.Backend.Infrastructure.Database
{
    public abstract class DatabaseTestBase<TCollection, TDataType>
        where TCollection : ICollectionBase<TDataType>
    {
        private readonly MongoDbRunner _runner;
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<TDataType> _collection;
        private readonly Repository _repository;
        private readonly IFixture _fixture;

        public DatabaseTestBase()
        {
            _fixture = FixtureFactory.GetCustomizedFixture();

            _runner = MongoDbRunner.Start();
            _client = new MongoClient(_runner.ConnectionString);
            _database = _client.GetDatabase("homecloud-maps");
            _collection = _database.GetCollection<TDataType>(typeof(TDataType).Name);

            _repository = new Repository(_client);            
        }

        protected abstract ICollectionBase<TDataType> GetCollection(IRepository repository);

        protected async Task InsertAsync()
        {
            // Arrange
            var expected = _fixture.Create<TDataType>();

            // Act
            await GetCollection(_repository).InsertAsync(expected);

            // Assert
            var data = await _collection.Find((_) => true).ToListAsync();
            data.Should().HaveCount(1);
            var actual = data.First();
            
            actual.Should().BeEquivalentTo(expected, 
                options => options.AddDateTimeCloseToExpected());
        }
    }
}
