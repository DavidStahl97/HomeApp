using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Infrastructure.Komoot.Entities
{
    public class Link
    {
        [JsonPropertyName("href")]
        public string Reference { get; set; }
    }
}
