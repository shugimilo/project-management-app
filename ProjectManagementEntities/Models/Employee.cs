using System;
using System.Collections.Generic;

namespace ProjectManagementEntities.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string? Email { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsActive { get; set; }
        public decimal? HourlyRate { get; set; }

        public TeamMember? TeamMembership { get; set; }
        public ICollection<TaskAssignment> TaskAssignments { get; set; } = new List<TaskAssignment>();
        public ICollection<TimesheetEntry> TimesheetEntries { get; set; } = new List<TimesheetEntry>();
    }
}
