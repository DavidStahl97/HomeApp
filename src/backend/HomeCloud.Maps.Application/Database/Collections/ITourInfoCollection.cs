using HomeCloud.Maps.Domain.Tours;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OneOf.Types.TrueFalseOrNull;

namespace HomeCloud.Maps.Application.Database.Collections
{
    public interface ITourInfoCollection : ICollectionBase<TourInfo>
    {
        Task<(IEnumerable<TourInfo> Page, long Count)> FindPageAsync(string userId, int index, int pageSize, OneOf<string, Null> tourNameFilter);
    }
}
