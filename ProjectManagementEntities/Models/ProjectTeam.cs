using System;
using System.Collections.Generic;

namespace ProjectManagementEntities.Models
{
    public class ProjectTeam
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;
        public int TeamId { get; set; }
        public Team Team { get; set; } = null!;

        public DateOnly From { get; set; }
        public DateOnly? To { get; set; }
    }
}
