using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectManagementEntities.Models;
using ProjectManagementApp.Services;

namespace ProjectManagementApp.Pages.Projects
{
    public class CreateModel : PageModel
    {
        private readonly ProjectService _projectService;

        public CreateModel(ProjectService projectService)
        {
            _projectService = projectService;
        }

        [BindProperty]
        public Project Project { get; set; } = new Project();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _projectService.CreateProjectAsync(Project);
            return RedirectToPage("./Index");
        }
    }
}
