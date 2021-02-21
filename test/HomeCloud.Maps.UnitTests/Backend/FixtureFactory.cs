using AutoFixture;
using HomeCloud.Maps.Application.Handlers.Tours;
using HomeCloud.Maps.Domain.Tours;
using HomeCloud.Maps.Domain.Types;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Maps.UnitTests.Backend
{
    public class FixtureFactory
    {
        public static IFixture GetCustomizedFixture()
        {
            return new Fixture().Customize(new CompositeCustomization(new Customize()));
        }

        class Customize : ICustomization
        {
            void ICustomization.Customize(IFixture fixture)
            {
                fixture.Customize<GetTourInfosPaginationRequest>(x => x
                    .With(p => p.TourNameFilter, Guid.NewGuid().ToString()));

                fixture.Customize<TourInfo>(x => x
                    .With(p => p.Id, ObjectId.Empty)
                    .With(p => p.Date, DateTime.UtcNow));
            }
        }
    }
}
