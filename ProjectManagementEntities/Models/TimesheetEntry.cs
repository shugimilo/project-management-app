using System;
using System.Collections.Generic;

namespace ProjectManagementEntities.Models
{
    public class TimesheetEntry
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;

        public int? ActivityId { get; set; }
        public Activity? Activity { get; set; }

        public DateTime WorkDate { get; set; } = DateTime.UtcNow.Date;

        public decimal Hours { get; set; }

        public string? Notes { get; set; }
    }
}
