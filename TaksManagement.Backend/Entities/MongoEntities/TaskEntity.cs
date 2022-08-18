using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaksManagement.Backend.Enums;

namespace TaksManagement.Backend.Entities.MongoEntities
{
    public class TaskEntity : MongoEntity
    {
        //public ObjectId TaskId { get; set; }
        public int SequenceNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ObjectId GeneratorId { get; set; }
        public string GeneratorName { get; set; }
        public ObjectId ResponsibleId { get; set; }
        public string ResponsibleName { get; set; }
        public int NumberOfActivities { get; set; }
        public EStatusTasks Status { get; set; }
        public DateTime OpeningDate { get; set; }
        public IEnumerable<ObjectId> ActivitiesIds { get; set; }
        public IEnumerable<ObjectId> ActivityGeneratorsIds { get; internal set; }
        public DateTime? LastActivity { get; internal set; }
    }
}
