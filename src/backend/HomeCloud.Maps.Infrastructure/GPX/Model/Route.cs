using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HomeCloud.Maps.Infrastructure.GPX.Model
{
    [XmlRoot(Namespace = "http://www.topografix.com/GPX/1/1", ElementName = "gpx")]
    public class Route
    {
        [XmlElement(ElementName = "trk")]
        public Track Track { get; set; }
    }
}
