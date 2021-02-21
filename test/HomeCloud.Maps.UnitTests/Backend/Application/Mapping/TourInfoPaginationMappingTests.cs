using FluentAssertions;
using HomeCloud.Maps.Application.Dto.Tours;
using HomeCloud.Maps.Domain.Tours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HomeCloud.Maps.UnitTests.Backend.Application.Mapping
{
    public class TourInfoPaginationMappingTests : MappingTestBase<TourInfo, TourInfoDto>
    {
        [Fact]
        public void TourInfoPaginationMapping_AllPropertiesShouldBeTested()
            => AllPropertiesShouldBeTested();

        [Fact]
        public void ShouldMap_TourId() => Map(x => x.TourId, x => x.TourId);

        [Fact]
        public void ShouldMap_Name() => Map(x => x.Name, x => x.Name);

        [Fact]
        public void ShouldMap_Date() => Map(x => x.Date, x => x.Date);

        [Fact]
        public void ShouldMap_Distance() => Map(x => x.Distance, x => x.Distance);

        [Fact]
        public void ShouldMap_ImageUrl() => Map(x => x.ImageUrl, x => x.ImageUrl);
    }
}
