using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Application.Dto
{
    public class PaginationResult<T> where T : class
    {
        public int Total { get; init; }

        public IEnumerable<T> Data { get; init; }
    }
}
