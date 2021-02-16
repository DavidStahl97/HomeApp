using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Application.Dto.Tours
{
    public class RouteDto
    {
        public IEnumerable<PositionDto> Positions { get; set; }
    }
}
