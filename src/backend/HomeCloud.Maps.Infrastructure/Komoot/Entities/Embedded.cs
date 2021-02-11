using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Infrastructure.Komoot.Entities
{
    public class Embedded
    {
        [JsonPropertyName("tours")]
        public List<Tour> Tours { get; set; }
    }
}
