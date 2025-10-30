using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementEntities.Models
{
    public class Activity : BaseEntity
    {
        public int TaskItemId { get; set; }
        public TaskItem? TaskItem { get; set; } = null!;
        public decimal PlannedHours { get; set; }

        [NotMapped]
        public decimal ActualHours => TimesheetEntries?.Sum(te => te.Hours) ?? 0;
        public DateTime? PerformedOn { get; set; }
        public ICollection<TimesheetEntry> TimesheetEntries { get; set; } = new List<TimesheetEntry>();

        public override decimal CalculateProgress()
        {
            var progress = PlannedHours > 0 ? (ActualHours / PlannedHours) * 100 : 0;
            return Math.Min(progress, 100);
        }
    }
}
