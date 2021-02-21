using Autofac.Extras.Moq;
using AutoFixture;
using HomeCloud.Maps.Server.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HomeCloud.Maps.UnitTests.Backend.Server.Controllers
{
    public class ControllerBaseTests<TController>
        where TController : ControllerBase
    {
        protected Fixture Fixture { get; } = new Fixture();

        public async Task TestRequestAsync<TRequest, TResponse>(Func<TController, Task<TResponse>> sendFunction, Action<TRequest> testRequest, string userId)
            where TRequest : class, IRequest<TResponse>
            where TResponse : new()
        {
            // Arrange
            using var mock = AutoMock.GetLoose();

            mock.Mock<IMediator>()
                .Setup(x => x.Send(It.IsAny<TRequest>(), CancellationToken.None))
                .Callback<IRequest<TResponse>, CancellationToken>((request, cancellation) =>
                {
                    var x = request as TRequest;
                    testRequest(x);
                })
                .ReturnsAsync(new TResponse());

            var controller = mock.Create<TController>();
            controller.AddJwt(new JsonWebToken(userId));

            // Act
            await sendFunction(controller);

            // Assert
            mock.Mock<IMediator>()
                .Verify(x => x.Send(It.IsAny<TRequest>(), CancellationToken.None), Times.Once);
        }

        public async Task TestResponseAsync<TRequest, TResponse>(Func<TController, Task<TResponse>> sendFunction, 
            Action<TResponse> testResponse, TResponse expectedResponse)
            where TRequest : class, IRequest<TResponse>
            where TResponse : new()
        {
            // Arrange
            using var mock = AutoMock.GetLoose();

            mock.Mock<IMediator>()
                .Setup(x => x.Send(It.IsAny<TRequest>(), CancellationToken.None))
                .ReturnsAsync(expectedResponse);

            var controller = mock.Create<TController>();
            controller.AddJwt(new JsonWebToken(Guid.NewGuid().ToString()));

            // Act
            var actualResponse = await sendFunction(controller);

            // Assert
            mock.Mock<IMediator>()
                .Verify(x => x.Send(It.IsAny<TRequest>(), CancellationToken.None), Times.Once);

            testResponse(actualResponse);
        }
    }
}
