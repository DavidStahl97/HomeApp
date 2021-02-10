using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Infrastructure.Komoot.Entities
{
    public class Tour
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("distance")]
        public double Distance { get; set; }

        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        [JsonPropertyName("sport")]
        public string Sport { get; set; }

        [JsonPropertyName("elevation_up")]
        public double ElevationUp { get; set; }

        [JsonPropertyName("elevation_down")]
        public double ElevationDown { get; set; }

        [JsonPropertyName("map_image")]
        public MapImage MapImage { get; set; }

        [JsonPropertyName("map_image_preview")]
        public MapImage MapImagePreview { get; set; }
    }
}
