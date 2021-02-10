using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Domain.Tours
{
    public class Route
    {
        public IEnumerable<Position> Positions { get; set; }
    }
}
