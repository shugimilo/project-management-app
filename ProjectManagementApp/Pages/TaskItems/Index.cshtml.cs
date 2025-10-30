using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectManagementApp.Services;
using ProjectManagementEntities.Models;

namespace ProjectManagementApp.Pages.TaskItems
{
    public class IndexModel : PageModel
    {
        private readonly TaskItemService _taskItemService;

        public IndexModel(TaskItemService taskItemService)
        {
            _taskItemService = taskItemService;
        }

        public List<TaskItem> TaskItems { get; set; } = new();
        public async Task OnGetAsync()
        {
            TaskItems = await _taskItemService.GetAllTasksAsync();
        }
    }
}
