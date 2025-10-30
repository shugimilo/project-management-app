using ProjectManagementEntities.Models;

namespace ProjectManagementApp.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalProjects { get; set; }
        public int TotalTasks { get; set; }
        public int PlannedTasks { get; set; }
        public int InProgressTasks { get; set; }
        public int CompletedTasks { get; set; }

        public int TotalEmployees { get; set; }
        public int ActiveEmployees { get; set; }

        public int TotalTeams { get; set; }
        public int TotalWorkPackages { get; set; }

        public List<TaskItem> RecentTasks { get; set; } = new();
        public List<Activity> RecentActivities { get; set; } = new();
    }
}
