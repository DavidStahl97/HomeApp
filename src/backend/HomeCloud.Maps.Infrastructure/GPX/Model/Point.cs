using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HomeCloud.Maps.Infrastructure.GPX.Model
{
    public class Point
    {
        [XmlAttribute(AttributeName = "lat")]
        public float Latitude { get; set; }

        [XmlAttribute(AttributeName = "lon")]
        public float Longitude { get; set; }
    }
}
