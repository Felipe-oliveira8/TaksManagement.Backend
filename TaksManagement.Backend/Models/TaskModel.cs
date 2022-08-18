using System.Collections.Generic;
using TaksManagement.Backend.Enums;

namespace TaksManagement.Backend.Models
{
    public class TaskModel
    {
        public string TaskId { get; set; }
        public int SequenceNumber { get; set; }
        public string ResponsibleId { get; set; }
        public string ResponsibleName { get; set; }
        public string GeneratorId { get; set; }
        public string GeneratorName { get; set; }
        public string OpeningDate { get; set; }
        public EStatusTasks Status { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> ActivitiesIds { get; set; }
    }
}
