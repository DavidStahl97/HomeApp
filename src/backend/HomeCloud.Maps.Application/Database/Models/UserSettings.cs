using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Application.Database.Models
{
    public class UserSettings
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string UserId { get; set; }

        public string KomootUserId { get; set; }
    }
}
