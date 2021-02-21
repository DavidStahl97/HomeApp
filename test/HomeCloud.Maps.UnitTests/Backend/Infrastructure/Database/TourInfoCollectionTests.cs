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
    }
}
