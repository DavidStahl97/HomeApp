using HomeCloud.Maps.Domain.Settings;
using HomeCloud.Maps.Domain.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Application.Database.Collections
{
    public interface IUserSettingsCollection : ICollectionBase<UserSettings>
    {
        Task<MaybeFound<UserSettings>> FirstAsync(string userId);

        Task ReplaceOrInsert(UserSettings settings);
    }
}
