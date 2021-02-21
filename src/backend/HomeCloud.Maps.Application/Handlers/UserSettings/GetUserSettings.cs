using AutoMapper;
using HomeCloud.Maps.Application.Database;
using HomeCloud.Maps.Application.Dto;
using HomeCloud.Maps.Domain.Types;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Application.Handlers.UserSettings
{
    public class GetUserSettingsRequest : IRequest<MaybeNull<UserSettingsDto>>
    {
        public string UserId { get; init; }
    }

    public class GetUserSettingsHandler : IRequestHandler<GetUserSettingsRequest, MaybeNull<UserSettingsDto>>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public GetUserSettingsHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<MaybeNull<UserSettingsDto>> Handle(GetUserSettingsRequest request, CancellationToken cancellationToken)
        {
            var settings = await _repository.UserSettingsCollection.FirstAsync(request.UserId);
            return settings.Match<UserSettingsDto>(x => _mapper.Map<UserSettingsDto>(x));
        }
    }
}
