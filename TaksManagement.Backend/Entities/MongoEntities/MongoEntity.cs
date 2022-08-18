using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaksManagement.Backend.Entities.MongoEntities
{
    public class MongoEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpatedAt { get; set; }
    }
}
