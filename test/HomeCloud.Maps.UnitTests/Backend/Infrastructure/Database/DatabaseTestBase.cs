using AutoFixture;
using FluentAssertions;
using HomeCloud.Maps.Application.Database;
using HomeCloud.Maps.Application.Database.Collections;
using HomeCloud.Maps.Domain.Types;
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
    public abstract class DatabaseTestBase<TCollection, TDataType> : IDisposable
        where TCollection : ICollectionBase<TDataType>
        where TDataType : class
    {
        private const string DATABASE_NAME = "homecloud-maps";

        private readonly IMongoCollection<TDataType> _collection;
        private readonly IMongoDatabase _database;
        private readonly IMongoClient _client;

        public DatabaseTestBase(DatabaseFixture databaseFixture)
        {
            Fixture = FixtureFactory.GetCustomizedFixture();

            _client = databaseFixture.Client;
            _database = _client.GetDatabase(DATABASE_NAME);
            _collection = _database.GetCollection<TDataType>(typeof(TDataType).Name);

            Repository = new Repository(databaseFixture.Client);            
        }

        protected IFixture Fixture { get; }

        protected IRepository Repository { get; }

        protected abstract ICollectionBase<TDataType> GetCollection(IRepository repository);

        protected async Task InsertAsync()
        {
            // Arrange
            var expected = Fixture.Create<TDataType>();

            // Act
            await GetCollection(Repository).InsertAsync(expected);

            // Assert
            var data = await GetAllAsync();
            data.Should().HaveCount(1);
            var actual = data.First();
            
            actual.Should().BeEquivalentTo(expected, 
                options => options.AddDateTimeCloseToExpected());
        }

        protected async Task InsertManyAsync()
        {
            // Arrange
            var expected = Fixture.Create<IEnumerable<TDataType>>();

            // Act
            await GetCollection(Repository).InsertManyAsync(expected);

            // Assert
            var actual = await GetAllAsync();

            actual.Should().HaveCount(expected.Count());
            actual.Should().BeEquivalentTo(expected, 
                options => options.AddDateTimeCloseToExpected());
        }

        protected async Task FirstAsync_ShouldReturnElement(
            Func<TDataType, Task<MaybeFound<TDataType>>> executeTest)
        {
            // Arrange
            var data = Fixture.CreateList<TDataType>(10);
            var expected = data.Skip(5).First();

            await GetCollection(Repository).InsertManyAsync(data);

            // Act
            var actual = await executeTest(expected);

            // Assert
            actual.Value.Should().BeEquivalentTo(expected,
                options => options.AddDateTimeCloseToExpected());
        }

        protected async Task FirsAsync_ShouldReturnNull(
            Func<Task<MaybeFound<TDataType>>> executeTest)
        {
            // Arrange
            var data = Fixture.CreateList<TDataType>(10);

            await GetCollection(Repository).InsertManyAsync(data);

            // Act
            var actual = await executeTest();

            // Assert
            actual.IsNull.Should().BeTrue();
        }

        private async Task<IEnumerable<TDataType>> GetAllAsync()
            => await _collection.Find((_) => true).ToListAsync();

        public void Dispose()
        {
            _client.DropDatabase(DATABASE_NAME);
        }
    }
}
