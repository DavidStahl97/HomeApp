using HomeCloud.Maps.Application.Database.Collections;
using HomeCloud.Maps.Domain.Settings;
using HomeCloud.Maps.Infrastructure.Database.Collection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeCloud.Maps.Infrastructure.Database.Collections
{
    class UserSettingsCollection : CollectionBase<UserSettings>, IUserSettingsCollection
    {
        public UserSettingsCollection(MongoClient client) : base(client)
        {
        }
    }
}
