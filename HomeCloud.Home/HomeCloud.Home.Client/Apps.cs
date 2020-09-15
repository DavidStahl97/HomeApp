using HomeCloud.Client.Common;
using HomeCloud.Client.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCloud.Home.Client
{
    public static class Apps
    {
        public static IStartup MapApp = new Map.Client.Startup();
    }
}
