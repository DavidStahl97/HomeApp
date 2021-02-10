using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Infrastructure.Komoot.Entities
{
    public class Links
    {
        [JsonPropertyName("self")]
        public Link Self { get; set; }

        [JsonPropertyName("next")]
        public Link Next { get; set; }

        [JsonPropertyName("prev")]
        public Link Previous { get; set; }
    }
}
