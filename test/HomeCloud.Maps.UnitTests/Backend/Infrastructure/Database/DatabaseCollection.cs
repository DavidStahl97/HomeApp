using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HomeCloud.Maps.UnitTests.Backend.Infrastructure.Database
{
    [CollectionDefinition("DB")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
    }
}
