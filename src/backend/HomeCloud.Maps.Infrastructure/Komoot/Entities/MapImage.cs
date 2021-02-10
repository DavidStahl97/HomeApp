using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Infrastructure.Komoot.Entities
{
    public class MapImage
    {
        [JsonPropertyName("src")]
        public string Source { get; set; }
    }
}
