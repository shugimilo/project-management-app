using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectManagementApp.Services;
using ProjectManagementEntities.Models;

namespace ProjectManagementApp.Pages.Activities
{
    public class DeleteModel : PageModel
    {
        private readonly ActivityService _activityService;

        public DeleteModel(ActivityService activityService)
        {
            _activityService = activityService;
        }

        [BindProperty]
        public Activity Activity { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Activity = await _activityService.GetActivityByIdAsync(id);
            if (Activity == null)
            {
                return RedirectToPage("/TaskItems/Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var activity = await _activityService.GetActivityByIdAsync(id);
            if (activity == null)
            {
                return RedirectToPage("/TaskItems/Index");
            }

            await _activityService.DeleteActivityAsync(id);

            return RedirectToPage("/TaskItems/Details", new { id = activity.TaskItemId });
        }
    }
}
