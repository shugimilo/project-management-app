using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectManagementEntities.Models;
using ProjectManagementApp.Services;

namespace ProjectManagementApp.Pages.WorkPackages
{
    public class CreateModel : PageModel
    {
        private readonly WorkPackageService _workPackageService;
        private readonly ProjectService _projectService;

        public CreateModel(WorkPackageService workPackageService, ProjectService projectService)
        {
            _workPackageService = workPackageService;
            _projectService = projectService;
        }

        [BindProperty]
        public WorkPackage WorkPackage { get; set; } = new WorkPackage();
        [BindProperty]
        public List<Project> Projects { get; set; } = new List<Project>();

        public async Task OnGetAsync()
        {
            Projects = await _projectService.GetAllProjectsAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    foreach (var error in state.Errors)
                    {
                        Console.WriteLine($"Error in {key}: {error.ErrorMessage}");
                    }
                }
                Projects = await _projectService.GetAllProjectsAsync();
                return Page();
            }
            await _workPackageService.CreateWorkPackageAsync(WorkPackage);
            return RedirectToPage("Index");
        }
    }
}
