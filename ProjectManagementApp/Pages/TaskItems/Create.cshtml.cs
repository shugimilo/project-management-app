using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectManagementEntities.Models;
using ProjectManagementApp.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjectManagementApp.Pages.TaskItems
{
    public class CreateModel : PageModel
    {
        private readonly TaskItemService _taskItemService;
        private readonly WorkPackageService _workPackageService;
        public CreateModel(TaskItemService taskItemService, WorkPackageService workPackageService)
        {
            _taskItemService = taskItemService;
            _workPackageService = workPackageService;
        }
        [BindProperty]
        public TaskItem TaskItem { get; set; } = new TaskItem();
        public List<WorkPackage> WorkPackages { get; set; } = new List<WorkPackage>();

        public List<SelectListItem> WorkPackageOptions { get; set; } = new();
        public async Task OnGetAsync()
        {
            WorkPackages = await _workPackageService.GetAllWorkPackagesAsync();

            WorkPackageOptions = WorkPackages.Select(wp => new SelectListItem
            {
                Value = wp.Id.ToString(),
                Text = $"[{wp.Project.Name}] {wp.Name}"
            }).ToList();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _taskItemService.CreateTaskAsync(TaskItem);
            return RedirectToPage("Index");
        }
    }
}
