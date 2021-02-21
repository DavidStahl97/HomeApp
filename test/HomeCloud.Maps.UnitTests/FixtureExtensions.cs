using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Maps.UnitTests
{
    public static class FixtureExtensions
    {
        public static IEnumerable<T> CreateList<T>(this IFixture fixture, int amount)
            => Enumerable.Range(0, amount)
                .Select(i => fixture.Create<T>())
                .ToList();
    }
}
