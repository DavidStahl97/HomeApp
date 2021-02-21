using HomeCloud.Maps.Application.Database;
using HomeCloud.Maps.Application.Database.Collections;
using HomeCloud.Maps.Infrastructure.Database.Collections;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeCloud.Maps.Infrastructure.Database
{
    public class Repository : IRepository
    {
        private readonly MongoClient _client;

        public Repository(MongoClient client)
        {
            _client = client;

            UserSettingsCollection = new UserSettingsCollection(_client);
            TourInfoCollection = new TourInfoCollection(_client);
            RouteCollection = new RouteCollection(_client);
        }

        public IUserSettingsCollection UserSettingsCollection { get; }

        public ITourInfoCollection TourInfoCollection { get; }

        public IRouteCollection RouteCollection { get; }
    }
}
