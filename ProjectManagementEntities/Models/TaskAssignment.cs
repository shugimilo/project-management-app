using System;
using System.Collections.Generic;

namespace ProjectManagementEntities.Models
{
    public class TaskAssignment
    {
        public int TaskItemId { get; set; }
        public TaskItem TaskItem { get; set; } = null!;

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;
        public string RoleOnTask { get; set; } = "";

        public DateTime AssignedOn { get; set; } = DateTime.UtcNow;
        public AssignmentStatus Status { get; set; } = AssignmentStatus.Assigned;
    }
}
