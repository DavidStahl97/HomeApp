using Autofac.Extras.Moq;
using AutoFixture;
using FluentAssertions;
using HomeCloud.Maps.Application.Database;
using HomeCloud.Maps.Application.Dto;
using HomeCloud.Maps.Application.Dto.Tours;
using HomeCloud.Maps.Application.Handlers.Tours;
using HomeCloud.Maps.Domain.Tours;
using HomeCloud.Maps.Domain.Types;
using Jmansar.SemanticComparisonExtensions;
using Moq;
using SemanticComparison.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HomeCloud.Maps.UnitTests.Backend.Application.Handlers
{
    public class GetTourInfoPaginationTests
    {
        private readonly IFixture _fixture = FixtureFactory.GetCustomizedFixture();

        [Fact]
        public async Task Handle_RequestTourInfoDBQuery()
        {
            // Arrange
            var request = CreateRequest();
            var dbResponse = CreateDbResponse();

            using var mock = AutoMock.GetLoose();

            mock.Mock<IRepository>()
                .Setup(x => x.TourInfoCollection.FindPageAsync(
                    It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<MaybeNull<string>>()))
                .ReturnsAsync(dbResponse)
                .Callback<string, int, int, MaybeNull<string>>((userId, index, pageSize, filter) =>
                {
                    // Assert
                    userId.Should().Be(request.UserId);
                    index.Should().Be(request.Page.Index);
                    pageSize.Should().Be(request.Page.Size);
                    filter.Value.Should().Be(request.TourNameFilter.Value);
                });

            var handler = mock.Create<GetTourInfoPaginationHandler>();

            // Act
            await handler.Handle(request, CancellationToken.None);

            // Assert
            mock.Mock<IRepository>()
                .Verify(x => x.TourInfoCollection.FindPageAsync(
                    It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<MaybeNull<string>>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ReturnCorrectDto()
        {
            // Arrange
            var request = CreateRequest();
            var dbResponse = CreateDbResponse();

            using var mock = AutoMock.GetLoose();

            mock.Mock<IRepository>()
                .Setup(x => x.TourInfoCollection.FindPageAsync(
                    It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<MaybeNull<string>>()))
                .ReturnsAsync(dbResponse);

            var handler = mock.Create<GetTourInfoPaginationHandler>();

            // Act
            var actual = await handler.Handle(request, CancellationToken.None);

            // Assert
            actual.Total.Should().Be(dbResponse.Count);
            actual.Data.Should().HaveSameCount(dbResponse.Data);

            foreach (var expected in dbResponse.Data)
            {
                var actualTourInfo = actual.Data.Single(x => x.Name == expected.Name);
                actualTourInfo.AsSource().OfLikeness<TourInfo>()
                    .Without(x => x.UserId)
                    .Without(x => x.Id)
                    .ShouldEqual(expected);
            }
        }

        private GetTourInfosPaginationRequest CreateRequest() 
            => _fixture.Create<GetTourInfosPaginationRequest>();

        private (IEnumerable<TourInfo> Data, int Count) CreateDbResponse()
            => (_fixture.Create<IEnumerable<TourInfo>>(), _fixture.Create<int>());
    }
}
