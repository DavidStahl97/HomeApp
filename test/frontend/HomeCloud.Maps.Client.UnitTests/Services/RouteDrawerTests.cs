using FluentAssertions;
using HomeCloud.Maps.Client.GPX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HomeCloud.Maps.Client.UnitTests.Services
{
    public class RouteDrawerTests
    {
        [Fact]
        public void Draw()
        {
            // Arrange
            var points = new List<Point>
            {
                new Point { Latitude = 50.433216f, Longitude = 8.497126f },
                new Point { Latitude = 50.375196f, Longitude = 8.499609f },
                new Point { Latitude = 50.366995f, Longitude = 8.508053f },
                new Point { Latitude = 50.433216f, Longitude = 8.497126f }
            };

            var drawer = new RouteDrawer();

            // Act
            var polygons = drawer.Draw(points);

            // Assert
            var first = polygons.First().Shape[0];
            first.Should().Equal(new System.Drawing.PointF(50.433216f, 8.497126f), new System.Drawing.PointF(50.375196f, 8.499609f));

            var second = polygons.Skip(1).First().Shape[0];
            second.Should().Equal(new System.Drawing.PointF(50.375196f, 8.499609f), new System.Drawing.PointF(50.366995f, 8.508053f));

            var third = polygons.Skip(2).First().Shape[0];
            third.Should().Equal(new System.Drawing.PointF(50.366995f, 8.508053f), new System.Drawing.PointF(50.433216f, 8.497126f));
        }
    }
}
