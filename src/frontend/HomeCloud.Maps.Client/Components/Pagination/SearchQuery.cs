using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Client.Components
{
    public record SearchQuery
    {
        public int PageIndex { get; init; }

        public int PageSize { get; init; }

        public string SearchString { get; init; }
    }
}
