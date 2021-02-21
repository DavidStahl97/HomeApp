using AutoMapper;
using HomeCloud.Maps.Application.Dto;
using HomeCloud.Maps.Application.Dto.Tours;
using HomeCloud.Maps.Domain.Settings;
using HomeCloud.Maps.Domain.Tours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TourInfo, TourInfoDto>();
            CreateMap<Route, RouteDto>();
            CreateMap<Position, PositionDto>();
            CreateMap<UserSettings, UserSettingsDto>();
        }
    }
}
