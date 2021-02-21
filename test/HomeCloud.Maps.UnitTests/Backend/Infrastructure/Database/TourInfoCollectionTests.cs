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

namespace HomeCloud.Maps.UnitTests.Backend.Infrastructure.Database
{
    public class TourInfoCollectionTests : DatabaseTestBase<ITourInfoCollection, TourInfo>
    {
        protected override ICollectionBase<TourInfo> GetCollection(IRepository repository)
            => repository.TourInfoCollection;

        [Fact]
        public Task TourInfo_InsertAsync() => InsertAsync();

        [Fact]
        public Task TourInfo_InsertManyAsync() => InsertManyAsync();

        [Fact]
        public Task TourInfo_FirstAsync()
            => FirstAsync(expected =>
                Repository.TourInfoCollection.FirstAsync(expected.UserId, expected.TourId));

        [Theory]
        [InlineData("Hausberg", "berg")]
        [InlineData("Feldberg - Bad Homburg", "Feld")]
        [InlineData("Feldberg - Bad", "feld")]
        [InlineData("Feldberg", "eld")]
        [InlineData("Feldberg - Bad Homburg", "- Bad ho")]
        [InlineData("Feldberg - Bad Homburg", "Feldberg - Bad Homburg")]
        public async Task TourInfo_SearchWithFilter_ShouldFindTour(string tourName, string filter)
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
    }
}
