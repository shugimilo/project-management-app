using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectManagementApp.Services;
using ProjectManagementEntities.Models;

namespace ProjectManagementApp.Pages.TaskItems
{
    public class DetailsModel : PageModel
    {
        private readonly TaskItemService _taskItemService;
        private readonly ProjectService _projectService;

        public DetailsModel(TaskItemService taskItemService, ProjectService projectService)
        {
            _taskItemService = taskItemService;
            _projectService = projectService;
        }

        public TaskItem? TaskItem { get; set; } = null!;

        // For the dropdown
        [BindProperty]
        public int SelectedEmployeeId { get; set; }
        public List<SelectListItem> AvailableEmployees { get; set; } = new();

        [BindProperty]
        public string RoleOnTask { get; set; } = "";

        public decimal PlannedHours { get; set; }
        public decimal ActualHours { get; set; }
        public decimal ProgressPercent { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            TaskItem = await _taskItemService.GetTaskByIdWithWorkPackageAsync(id);
            if (TaskItem == null)
            {
                return RedirectToPage("Index");
            }

            var employees = await _projectService.GetEmployeesForProjectAsync(TaskItem.WorkPackage.ProjectId);

            AvailableEmployees = employees.Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.FirstName + " " + e.LastName
            }).ToList();

            PlannedHours = TaskItem.Activities.Sum(a => a.PlannedHours);
            ActualHours = TaskItem.Activities.Sum(a => a.ActualHours);
            ProgressPercent = TaskItem.CalculateProgress();

            return Page();
        }

    }
}
