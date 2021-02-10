﻿using HomeCloud.Maps.Application.Database.Collections;
using HomeCloud.Maps.Domain.Tours;
using HomeCloud.Maps.Infrastructure.Database.Collection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Infrastructure.Database.Collections
{
    class TourInfoCollection : CollectionBase<TourInfo>, ITourInfoCollection
    {
        public TourInfoCollection(MongoClient client) : base(client)
        {
        }
    }
}
