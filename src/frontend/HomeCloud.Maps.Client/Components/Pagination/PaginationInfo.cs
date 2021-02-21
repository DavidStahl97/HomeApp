using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Client.Components
{
    public class PaginationInfo
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int Total { get; set; }

        public static PaginationInfo Default = new PaginationInfo
        {
            PageIndex = 0,
            PageSize = 10,
            Total = 0
        };
    }
}
