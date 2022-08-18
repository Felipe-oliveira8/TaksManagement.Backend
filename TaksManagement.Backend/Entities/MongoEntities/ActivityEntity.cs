using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaksManagement.Backend.Entities.MongoEntities;

namespace TaksManagement.Backend.Entities
{
    public class ActivityEntity : MongoEntity
    {
        public int Sequence { get; set; }
        public ObjectId TaskId { get; set; }
        public DateTime Date { get; set; }
        public ObjectId GeneratorId { get; set; }
        public string GeneratorName { get; set; }
        public string Description { get; set; }
    }
}
