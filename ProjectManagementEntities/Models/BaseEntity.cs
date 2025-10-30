using System;
using System.Collections.Generic;

namespace ProjectManagementEntities.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = string.Empty;
        public WorkStatus Status { get; set; } = WorkStatus.Planned;

        public abstract decimal CalculateProgress();
    }
}
