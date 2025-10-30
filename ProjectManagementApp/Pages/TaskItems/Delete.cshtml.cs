using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectManagementApp.Services;
using ProjectManagementEntities.Models;

namespace ProjectManagementApp.Pages.TaskItems
{
    public class DeleteModel : PageModel
    {
        private readonly TaskItemService _taskItemService;
        public DeleteModel(TaskItemService taskItemService)
        {
            _taskItemService = taskItemService;
        }
        [BindProperty]
        public TaskItem? TaskItem { get; set; } = null!;
        public async Task<IActionResult> OnGetAsync(int id)
        {
            TaskItem = await _taskItemService.GetTaskByIdAsync(id);
            if (TaskItem == null)
            {
                return RedirectToPage("Index");
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _taskItemService.DeleteTaskAsync(id);
            return RedirectToPage("Index");
        }
    }
}
