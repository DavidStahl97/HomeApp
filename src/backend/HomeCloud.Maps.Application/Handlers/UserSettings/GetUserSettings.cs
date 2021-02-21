using HomeCloud.Maps.Application.Database;
using HomeCloud.Maps.Application.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Application.Handlers.UserSettings
{
    public class GetUserSettingsRequest : IRequest<UserSettingsDto>
    {
        public string UserId { get; init; }
    }

    public class GetUserSettingsHandler : IRequestHandler<GetUserSettingsRequest, UserSettingsDto>
    {
        private readonly IRepository _repository;

        public GetUserSettingsHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserSettingsDto> Handle(GetUserSettingsRequest request, CancellationToken cancellationToken)
        {
            var settings = await _repository.UserSettingsCollection.FirstAsync(request.UserId);

            if (settings == null)
            {
                return new UserSettingsDto { KomootUserId = string.Empty };
            }

            return new UserSettingsDto
            {
                KomootUserId = settings.KomootUserId
            };
        }
    }
}
