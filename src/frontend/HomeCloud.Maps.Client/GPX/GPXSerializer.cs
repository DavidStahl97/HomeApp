using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using HomeCloud.Maps.Client.GPX.Model;

namespace HomeCloud.Maps.Client.GPX
{
    public class GPXSerializer : IGPXSerializer
    {
        public Route Deserialize(Stream stream)
        {
            var serializer = new XmlSerializer(typeof(Route));
            var route = serializer.Deserialize(stream) as Route;
            return route;
        }
    }
}
