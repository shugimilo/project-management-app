using System;
using System.Collections.Generic;

namespace ProjectManagementEntities.Models
{
    public class TeamMember
    {
        public int TeamId { get; set; }
        public Team Team { get; set; } = null!;

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;
        public string RoleInTeam { get; set; } = "";


        public DateTime JoinedOn { get; set; } = DateTime.UtcNow;
        public DateTime? LeftOn { get; set; }
    }
}
