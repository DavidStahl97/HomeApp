using AutoFixture;
using AutoMapper;
using FluentAssertions;
using HomeCloud.Maps.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HomeCloud.Maps.UnitTests.Backend.Application.Mapping
{
    public class MappingTestBase<TSource, TDestination>
    {
        private const string SHOULD_MAP_PREFIX = "ShouldMap_";

        public MappingTestBase()
        {
            var mapper = MappingFactory.CreateMapper();

            Source = FixtureFactory.GetCustomizedFixture().Create<TSource>();
            Destination = mapper.Map<TDestination>(Source);
        }

        protected TSource Source { get; }

        protected TDestination Destination { get; }

        protected void AllPropertiesShouldBeTested()
        {
            var mapTestMethods = GetType().GetMethods()
                .Where(x => x.Name.StartsWith(SHOULD_MAP_PREFIX))
                .ToList();

            foreach (var property in typeof(TDestination).GetProperties())
            {
                var hasTestMethod = mapTestMethods
                    .Any(x => x.Name == $"{SHOULD_MAP_PREFIX}{property.Name}");
                if (hasTestMethod == false)
                {
                    throw new Exception($"The mapping of the property {property.Name} has no test");
                }
            }
        }

        protected void Map<TSourceProperty, TDestinationProperty>(
            Func<TSource, TSourceProperty> sourceProperty, 
            Func<TDestination, TDestinationProperty> destinationProperty)
        {
            destinationProperty(Destination).Should().Be(sourceProperty(Source));
        }
    }
}
