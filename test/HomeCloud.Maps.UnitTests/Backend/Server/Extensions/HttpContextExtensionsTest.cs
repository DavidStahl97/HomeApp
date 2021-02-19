using FluentAssertions;
using HomeCloud.Maps.Server.Extensions;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HomeCloud.Maps.UnitTests.Backend.Server.Extensions
{
    public class HttpContextExtensionsTest
    {
        [Theory]
        [InlineData("subject123")]
        [InlineData("2342342342343248923u414324234f232323")]
        public void GetJsonWebToken(string expectedSubject)
        {
            // Arrange
            var mock = new Mock<HttpContext>();
            mock.Setup(x => x.User.Claims)
                .Returns(new List<Claim>
                {
                    new Claim("sub", expectedSubject)
                });

            var httpContext = mock.Object;

            // Act
            var jwt = httpContext.GetJsonWebToken();

            // Assert
            jwt.Subject.Should().Be(expectedSubject);
        }
    }
}
