using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectManagementApp.Services;
using ProjectManagementApp.ViewModels;
using ProjectManagementEntities.Models;

public class IndexModel : PageModel
{
    private readonly ProjectService _projectService;
    private readonly WorkPackageService _workPackageService;
    private readonly TaskItemService _taskItemService;
    private readonly EmployeeService _employeeService;
    private readonly TeamService _teamService;

    public IndexModel(ProjectService projectService, TaskItemService taskItemService,
                      EmployeeService employeeService, TeamService teamService, WorkPackageService workPackageService)
    {
        _projectService = projectService;
        _taskItemService = taskItemService;
        _employeeService = employeeService;
        _teamService = teamService;
        _workPackageService = workPackageService;
    }

    public DashboardViewModel Dashboard { get; set; } = new();

    public async Task OnGetAsync()
    {
        var projects = await _projectService.GetAllProjectsAsync();
        var workPackages = await _workPackageService.GetAllWorkPackagesAsync();
        var tasks = await _taskItemService.GetAllTasksAsync();
        var employees = await _employeeService.GetAllEmployeesAsync();
        var teams = await _teamService.GetAllTeamsAsync();

        Dashboard.TotalProjects = projects.Count;
        Dashboard.TotalWorkPackages = workPackages.Count;
        Dashboard.TotalTasks = tasks.Count;
        Dashboard.PlannedTasks = tasks.Count(t => t.Status == WorkStatus.Planned);
        Dashboard.InProgressTasks = tasks.Count(t => t.Status == WorkStatus.InProgress);
        Dashboard.CompletedTasks = tasks.Count(t => t.Status == WorkStatus.Completed);

        Dashboard.TotalEmployees = employees.Count;
        Dashboard.ActiveEmployees = employees.Count(e => e.IsActive);

        Dashboard.TotalTeams = teams.Count;
        Dashboard.TotalWorkPackages = projects.Sum(p => p.WorkPackages.Count);

        Dashboard.RecentTasks = tasks.OrderByDescending(t => t.Id).Take(5).ToList();
        Dashboard.RecentActivities = tasks.SelectMany(t => t.Activities)
                                          .OrderByDescending(a => a.Id)
                                          .Take(5)
                                          .ToList();
    }
}
