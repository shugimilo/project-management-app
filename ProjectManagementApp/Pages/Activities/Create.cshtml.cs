using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectManagementApp.Services;
using ProjectManagementEntities.Models;

namespace ProjectManagementApp.Pages.Activities
{
    public class CreateModel : PageModel
    {
        private readonly ActivityService _activityService;
        private readonly TaskItemService _taskItemService;

        public CreateModel(ActivityService activityService, TaskItemService taskItemService)
        {
            _activityService = activityService;
            _taskItemService = taskItemService;
        }

        [BindProperty]
        public Activity Activity { get; set; } = new Activity();

        public IActionResult OnGet(int? taskItemId)
        {
            if (taskItemId == null)
            {
                return RedirectToPage("/TaskItems/Index");
            }

            Activity.TaskItemId = taskItemId.Value;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var task = await _taskItemService.GetTaskByIdAsync(Activity.TaskItemId);
            if (task == null)
            {
                TempData["ErrorMessage"] = "Parent task not found.";
                return RedirectToPage("/TaskItems/Index");
            }

            await _activityService.CreateActivityAsync(Activity);

            return RedirectToPage("/TaskItems/Details", new { id = Activity.TaskItemId });
        }
    }
}
