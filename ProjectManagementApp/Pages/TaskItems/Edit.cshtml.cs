using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectManagementApp.Services;
using ProjectManagementEntities.Models;

namespace ProjectManagementApp.Pages.TaskItems
{
    public class EditModel : PageModel
    {
        private readonly TaskItemService _taskItemService;
        private readonly WorkPackageService _workPackageService;
        private readonly ProjectService _projectService;

        public EditModel(TaskItemService taskItemService, WorkPackageService workPackageService, ProjectService projectService)
        {
            _taskItemService = taskItemService;
            _workPackageService = workPackageService;
            _projectService = projectService;
        }

        [BindProperty]
        public TaskItem? TaskItem { get; set; } = null!;

        public List<WorkPackage> WorkPackages { get; set; } = new();
        public List<SelectListItem> WorkPackageOptions { get; set; } = new();

        [BindProperty]
        public int SelectedEmployeeId { get; set; }
        public List<SelectListItem> AvailableEmployees { get; set; } = new();

        [BindProperty]
        public string RoleOnTask { get; set; } = "";

        public decimal PlannedHours { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            TaskItem = await _taskItemService.GetTaskByIdAsync(id);
            WorkPackages = await _workPackageService.GetAllWorkPackagesAsync();

            WorkPackageOptions = WorkPackages.Select(wp => new SelectListItem
            {
                Value = wp.Id.ToString(),
                Text = $"[{wp.Project.Name}] {wp.Name}"
            }).ToList();

            if (TaskItem == null)
            {
                return RedirectToPage("Index");
            }

            PlannedHours = TaskItem.Activities.Sum(a => a.PlannedHours);

            var employees = await _workPackageService.GetTeamMembersAsync(TaskItem.WorkPackageId);
            AvailableEmployees = employees.Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.FirstName + " " + e.LastName
            }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _taskItemService.UpdateTaskAsync(TaskItem);
            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostAssignEmployeeAsync(int id)
        {
            // Check if task exists first
            var task = await _taskItemService.GetTaskByIdWithWorkPackageAsync(id);
            if (task == null)
            {
                TempData["ErrorMessage"] = "Task not found.";
                return RedirectToPage();
            }

            // Check if the employee is already assigned
            if (task.TaskAssignments?.Any(ta => ta.EmployeeId == SelectedEmployeeId) == true)
            {
                TempData["ErrorMessage"] = "This employee is already assigned to the task.";
                return RedirectToPage(new { id });
            }

            // Assign the employee
            await _taskItemService.AssignEmployeeToTaskAsync(id, SelectedEmployeeId, RoleOnTask, PlannedHours);

            return RedirectToPage(new { id });
        }

        public async Task<IActionResult> OnPostRemoveEmployeeAsync(int taskId, int employeeId)
        {
            await _taskItemService.RemoveEmployeeFromTaskAsync(taskId, employeeId);
            return RedirectToPage(new { id = taskId });
        }
    }
}
