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
    public class UpdateUserSettingsRequest : IRequest
    {
        public string UserId { get; init; }

        public UserSettingsDto UserSettings { get; init; }
    }

    public class UpdateUserSettingsHandler : IRequestHandler<UpdateUserSettingsRequest>
    {
        private readonly IRepository _repository;

        public UpdateUserSettingsHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateUserSettingsRequest request, CancellationToken cancellationToken)
        {
            var userSettings = new Domain.Settings.UserSettings
            {
                UserId = request.UserId,
                KomootUserId = request.UserSettings.KomootUserId
            };

            await _repository.UserSettingsCollection
                .ReplaceOrInsert(x => x.UserId == request.UserId, userSettings);

            return Unit.Value;
        }
    }
}
