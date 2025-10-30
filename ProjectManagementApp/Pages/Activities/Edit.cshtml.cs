using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectManagementApp.Services;
using ProjectManagementEntities.Models;

namespace ProjectManagementApp.Pages.Activities
{
    public class EditModel : PageModel
    {
        private readonly ActivityService _activityService;

        public EditModel(ActivityService activityService)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _activityService.UpdateActivityAsync(Activity);

            return RedirectToPage("/TaskItems/Details", new { id = Activity.TaskItemId });
        }
    }
}
