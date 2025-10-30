using System;
using System.Collections.Generic;

namespace ProjectManagementEntities.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public List<WorkPackage> WorkPackages { get; set; } = new List<WorkPackage>();
        public ICollection<TeamMember>? TeamMembers { get; set; }
        public ICollection<ProjectTeam> ProjectTeams { get; set; } = new List<ProjectTeam>();
    }
}
