using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HomeCloud.Maps.UnitTests.Backend.Domain
{
    public class LoggerExtensionsTests
    {
        [Fact]
        public void WriteInformation()
        {
            // Arrange
            var title = "Test WriteInformation";
            var s1 = "test-s1";
            var s2 = "test-s2";
            int number = 4;
            var obj = new TestRecord { Value = "test-s3" };

            var loggerMock = new LoggerMock<LoggerExtensionsTests>((logLevel, message) =>
            {
                // Assert
                logLevel.Should().Be(LogLevel.Information);
                message.Should().Be($"{title} \n s1: {s1} \n s2: {s2} \n number: {number} \n obj: {obj}");
            });

            // Act
            Maps.Domain.LoggerExtensions.WriteInformation(loggerMock, title,
                (nameof(s1), s1),
                (nameof(s2), s2),
                (nameof(number), number),
                (nameof(obj), obj));
        }
    }

    record TestRecord
    {
        public string Value { get; init; }

        public override string ToString()
        {
            return Value;
        }
    }
}
