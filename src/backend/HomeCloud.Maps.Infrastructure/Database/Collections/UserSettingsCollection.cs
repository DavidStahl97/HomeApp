using HomeCloud.Maps.Application.Database.Collections;
using HomeCloud.Maps.Domain.Settings;
using HomeCloud.Maps.Infrastructure.Database.Collection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Infrastructure.Database.Collections
{
    public class UserSettingsCollection : CollectionBase<UserSettings>, IUserSettingsCollection
    {
        public UserSettingsCollection(MongoClient client) : base(client)
        {
        }

        public Task<UserSettings> FirstAsync(string userId)
            => FirstAsync(x => x.UserId == userId);

        public Task ReplaceOrInsert(UserSettings settings)
            => ReplaceOrInsert(x => x.UserId == settings.UserId, settings);
    }
}
