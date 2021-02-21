using FluentAssertions;
using FluentAssertions.Equivalency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Maps.UnitTests
{
    public static class FluentAssertionExtensions
    {
        public static EquivalencyAssertionOptions<TExpectation> AddDateTimeCloseToExpected<TExpectation>(
            this EquivalencyAssertionOptions<TExpectation> options)
        {
            return options.Using<DateTime>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation))
                .WhenTypeIs<DateTime>();
        }
    }
}
