using FluentAssertions;
using HomeCloud.Maps.Application.Database;
using HomeCloud.Maps.Application.Database.Collections;
using HomeCloud.Maps.Domain.Tours;
using HomeCloud.Maps.Infrastructure.Database.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static OneOf.Types.TrueFalseOrNull;

namespace HomeCloud.Maps.UnitTests.Backend.Infrastructure.Database.Collections
{
    [Collection("DB")]
    public class TourInfoCollectionTests : DatabaseTestBase<ITourInfoCollection, TourInfo>
    {
        public TourInfoCollectionTests(DatabaseFixture databaseFixture) : base(databaseFixture)
        {
        }

        protected override ICollectionBase<TourInfo> GetCollection(IRepository repository)
            => repository.TourInfoCollection;

        [Fact]
        public Task TourInfo_InsertAsync() => InsertAsync();

        [Fact]
        public Task TourInfo_InsertManyAsync() => InsertManyAsync();

        [Fact]
        public Task FirstAsync_ShouldReturnTour()
            => FirstAsync_ShouldReturnElement(expected =>
                Repository.TourInfoCollection.FirstAsync(expected.UserId, expected.TourId));

        [Theory]
        [InlineData("test-userid", "test-tourid")]
        public Task FirstAsync_ShouldReturnNull(string userId, string tourId)
            => FirsAsync_ShouldReturnNull(() => Repository.TourInfoCollection.FirstAsync(userId, tourId));

        [Theory]
        [InlineData("Hausberg", "berg")]
        [InlineData("Feldberg - Bad Homburg", "Feld")]
        [InlineData("Feldberg - Bad", "feld")]
        [InlineData("Feldberg", "eld")]
        [InlineData("Feldberg - Bad Homburg", "- Bad ho")]
        [InlineData("Feldberg - Bad Homburg", "Feldberg - Bad Homburg")]
        public async Task FindPageAsync_SearchWithFilter_ShouldFindTour(string tourName, string filter)
        {
            // Arrange
            var data = Fixture.CreateList<TourInfo>(10);
            var expected = data.Skip(5).First();
            expected.Name = tourName;

            await Repository.TourInfoCollection.InsertManyAsync(data);

            // Act
            var (Page, Count) = await Repository.TourInfoCollection
                .FindPageAsync(expected.UserId, 0, 10, filter);

            // Assert
            Page.Should().HaveCount(1);

            var actual = Page.First();
            actual.Should().BeEquivalentTo(expected, 
                options => options.AddDateTimeCloseToExpected());
        }

        [Fact]
        public async Task FindPageAsync_SearchWithoutFilter()
        {
            // Arrange
            const string userId = "userid-test";
            
            var data = Fixture.CreateList<TourInfo>(10);
            
            var expected = new[]
            {
                data.First(), data.Skip(5).First(), data.Last()
            }.ToList();
            expected.ForEach(x => x.UserId = userId);

            await Repository.TourInfoCollection.InsertManyAsync(expected);

            // Act
            var (actual, count) = await Repository.TourInfoCollection
                .FindPageAsync(userId, 0, 10, new Null());

            // Assert
            count.Should().Be(3);
            actual.Should().BeEquivalentTo(expected,
                options => options.AddDateTimeCloseToExpected());
        }

        [Theory]
        [InlineData("Hausberg", "berg")]
        [InlineData("Feldberg - Bad Homburg", "Feld")]
        [InlineData("Feldberg - Bad", "feld")]
        [InlineData("Feldberg", "eld")]
        [InlineData("Feldberg - Bad Homburg", "- Bad ho")]
        [InlineData("Feldberg - Bad Homburg", "Feldberg - Bad Homburg")]
        public async Task FindPageAsync_ShouldReturnTotalCount(string tourName, string filter)
        {
            // Arrange
            const string userId = "userId-test";
            const int pageSize = 10;

            var data = Fixture.CreateList<TourInfo>(61).ToList();
            
            var fromUser = data.Take(50).ToList();
            fromUser.ForEach(x => x.UserId = userId);

            var expected = fromUser.Take(29).ToList();
            expected.ForEach(x => x.Name += tourName);

            await Repository.TourInfoCollection.InsertManyAsync(data);

            // Act
            var (page, count) = await Repository.TourInfoCollection.FindPageAsync(userId, 0, pageSize, filter);

            // Assert
            count.Should().Be(29);
        }

        [Theory]
        [InlineData("Hausberg", "berg")]
        [InlineData("Feldberg - Bad Homburg", "Feld")]
        [InlineData("Feldberg - Bad", "feld")]
        [InlineData("Feldberg", "eld")]
        [InlineData("Feldberg - Bad Homburg", "- Bad ho")]
        [InlineData("Feldberg - Bad Homburg", "Feldberg - Bad Homburg")]
        public async Task FindPageAsync_ShouldReturnCorrectPagination(string tourName, string filter)
        {
            // Arrange
            const string userId = "userId-test";
            const int pageSize = 10;

            var data = Fixture.CreateList<TourInfo>(61).ToList();

            var fromUser = data.Take(50).ToList();
            fromUser.ForEach(x => x.UserId = userId);

            var expected = fromUser.Take(29).ToList();
            expected.ForEach(x => x.Name += tourName);

            var expectedPageOne = expected.Take(10).ToList();
            var expectedPageTwo = expected.Skip(10).Take(10).ToList();
            var expectedPageThree = expected.Skip(20).ToList();

            await Repository.TourInfoCollection.InsertManyAsync(data);

            // Act
            var (actualPageOne, _) = await Repository.TourInfoCollection.FindPageAsync(userId, 0, pageSize, filter);
            var (actualPageTwo, _) = await Repository.TourInfoCollection.FindPageAsync(userId, 1, pageSize, filter);
            var (actualPageThree, _) = await Repository.TourInfoCollection.FindPageAsync(userId, 2, pageSize, filter);

            // Assert
            actualPageOne.Should().HaveCount(10);
            actualPageOne.Should().BeEquivalentTo(expectedPageOne,
                options => options.AddDateTimeCloseToExpected());

            actualPageTwo.Should().HaveCount(10);
            actualPageTwo.Should().BeEquivalentTo(expectedPageTwo,
                options => options.AddDateTimeCloseToExpected());

            actualPageThree.Should().HaveCount(9);
            actualPageThree.Should().BeEquivalentTo(expectedPageThree,
                options => options.AddDateTimeCloseToExpected());
        }
    }
}
