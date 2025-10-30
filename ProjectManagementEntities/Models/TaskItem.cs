using System;
using System.Collections.Generic;

namespace ProjectManagementEntities.Models
{
    // The class is named "TaskItem" to avoid conflict with the reserved keyword "Task".
    public class TaskItem : BaseEntity
    {
        public int WorkPackageId { get; set; }
        public WorkPackage? WorkPackage { get; set; }

        public DateTime? DueDate { get; set; }
        public int? Priority { get; set; }

        public ICollection<TaskAssignment> TaskAssignments { get; set; } = new List<TaskAssignment>();
        public ICollection<Activity> Activities { get; set; } = new List<Activity>();
        public ICollection<TimesheetEntry> TimesheetEntries { get; set; } = new List<TimesheetEntry>();

        public override decimal CalculateProgress()
            => Activities.Any() ? Activities.Average(a => a.CalculateProgress()) : 0;
    }
}
