using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Application.Handlers
{
    public record Page
    {
        public int Index { get; init; }

        public int Size { get; init; }
    }
}
