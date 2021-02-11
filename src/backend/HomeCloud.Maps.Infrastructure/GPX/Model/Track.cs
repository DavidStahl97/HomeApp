using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HomeCloud.Maps.Infrastructure.GPX.Model
{
    public class Track
    {
        [XmlArray("trkseg")]
        [XmlArrayItem("trkpt")]
        public List<Point> Points { get; set; }
    }
}
