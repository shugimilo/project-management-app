using System;
using System.Collections.Generic;

namespace ProjectManagementEntities.Models
{
    public class WorkPackage : BaseEntity
    {
        public int ProjectId { get; set; }
        public Project? Project { get; set; }

        public int? TeamId { get; set; }
        public Team? Team { get; set; }

        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();

        public override decimal CalculateProgress()
            => Tasks.Any() ? Tasks.Average(t => t.CalculateProgress()) : 0;
    }
}
