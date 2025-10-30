using System;
using System.Collections.Generic;

namespace ProjectManagementEntities.Models
{
    public class Project : BaseEntity
    {
        public string? Code { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        public ICollection<WorkPackage> WorkPackages { get; set; } = new List<WorkPackage>();
        public ICollection<ProjectTeam> ProjectTeams { get; set; } = new List<ProjectTeam>();
        public override decimal CalculateProgress()
            => WorkPackages.Any() ? WorkPackages.Average(wp => wp.CalculateProgress()) : 0;
    }
}
