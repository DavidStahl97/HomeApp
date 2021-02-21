using Mongo2Go;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Maps.UnitTests.Backend.Infrastructure.Database
{
    public class DatabaseFixture
    {
        private readonly MongoDbRunner _runner;

        public DatabaseFixture()
        {
            _runner = MongoDbRunner.Start();
            Client = new MongoClient(_runner.ConnectionString);            
        }

        public MongoClient Client { get; }
    }
}
