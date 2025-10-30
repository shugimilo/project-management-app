using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectManagementApp.Services;
using ProjectManagementEntities.Models;

namespace ProjectManagementApp.Pages.Projects
{
    public class DeleteModel : PageModel
    {
        private readonly ProjectService _projectService;

        public DeleteModel(ProjectService projectService)
        {
            _projectService = projectService;
        }

        [BindProperty]
        public Project? Project { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Project = await _projectService.GetProjectByIdAsync(id);
            if (Project == null)
            {
                return RedirectToPage("Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _projectService.DeleteProjectAsync(id);
            return RedirectToPage("Index");
        }
    }
}
