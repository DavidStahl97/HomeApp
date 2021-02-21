using Autofac.Extras.Moq;
using AutoFixture;
using FluentAssertions;
using HomeCloud.Maps.Application.Dto;
using HomeCloud.Maps.Application.Dto.Tours;
using HomeCloud.Maps.Application.Handlers;
using HomeCloud.Maps.Application.Handlers.Tours;
using HomeCloud.Maps.Server.Controllers;
using HomeCloud.Maps.Server.Extensions;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HomeCloud.Maps.UnitTests.Backend.Server.Controllers
{
    public class TourInfosControllerTests : ControllerBaseTests<TourInfosController>
    {
        [Theory]
        [InlineData(1, 10, "filter", "test-user")]
        public Task GetTourInfosPagination_ShouldSendTourNameFilter(int pageIndex, int pageSize, 
            string tourNameFilter, string userId)
        {
            return TestRequestAsync<GetTourInfosPaginationRequest, PaginationResult<TourInfoDto>>(
                x => x.GetTourInfosPagination(pageSize, pageIndex, tourNameFilter),
                actual => 
                {
                    actual.Page.Index.Should().Be(pageIndex);
                    actual.Page.Size.Should().Be(pageSize);
                    actual.TourNameFilter.Value.Should().Be(tourNameFilter);
                    actual.UserId.Should().Be(userId);
                },
                userId);
        }

        [Theory]
        [InlineData("userid-test")]
        public Task GetTourInfosPagination_ShouldSendTourNameFilterNull(string userId)
        {
            return TestRequestAsync<GetTourInfosPaginationRequest, PaginationResult<TourInfoDto>>(
                x => x.GetTourInfosPagination(0, 0, null),
                actual => actual.TourNameFilter.IsNull.Should().BeTrue(),
                userId);
        }

        [Fact]
        public Task GetTourInfosPagination_ShouldReturnResponse()
        {
            var expected = Fixture.Create<PaginationResult<TourInfoDto>>();

            return TestResponseAsync<GetTourInfosPaginationRequest, PaginationResult<TourInfoDto>>(
                x => x.GetTourInfosPagination(),
                actual => actual.Should().BeEquivalentTo(expected),
                expected);
        }
    }
}
