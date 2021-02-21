using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Client.Components
{
    public record SearchResult<TItem>
    {
        public IEnumerable<TItem> Items { get; init; }

        public int Total { get; init; }
    }
}
