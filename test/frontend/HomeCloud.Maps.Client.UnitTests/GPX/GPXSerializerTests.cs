using FluentAssertions;
using HomeCloud.Maps.Client.GPX;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace HomeCloud.Maps.Client.UnitTests
{
    public class GPXSerializerTests
    {
        [Fact]
        public void Deserialze()
        {
            // Arrange
            using var stream = new FileStream("2021-01-31_311521186_Hattsteinweier.gpx", FileMode.Open);
            var serializer = new GPXSerializer();

            // Act
            var route = serializer.Deserialize(stream);

            // Assert
            var points = route.Track.Points;
            
            var first = points.First();
            first.Latitude.Should().Be(50.433216f);
            first.Longitude.Should().Be(8.497126f);

            var second = points.Skip(1).First();
            second.Latitude.Should().Be(50.433220f);
            second.Longitude.Should().Be(8.497147f);

            var third = points.Skip(2).First();
            third.Latitude.Should().Be(50.433248f);
            third.Longitude.Should().Be(8.497235f);
        }
    }
}
