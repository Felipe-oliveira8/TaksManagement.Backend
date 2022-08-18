using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaksManagement.Backend.Enums;

namespace TaksManagement.Backend.Models
{
    public class ActivityModel
    {
        public string TaskId { get; set; }
        public string Description { get; set; }
        public string GeneratorName { get; set; }
        public EStatusTasks Status { get; set; }
        public string Date { get; set; }
        public int Sequence { get; set; }

    }
}
