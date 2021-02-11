using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Domain.Tours
{
    public class Route
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string TourId { get; set; }

        public string UserId { get; set; }

        public IEnumerable<Position> Positions { get; set; }
    }
}
