using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectManagementApp.Services;
using ProjectManagementEntities.Models;

namespace ProjectManagementApp.Pages.WorkPackages
{
    public class EditModel : PageModel
    {
        private readonly WorkPackageService _workPackageService;
        private readonly ProjectService _projectService;

        public EditModel(WorkPackageService workPackageService, ProjectService projectService)
        {
            _workPackageService = workPackageService;
            _projectService = projectService;
        }

        [BindProperty]
        public WorkPackage? WorkPackage { get; set; } = null!;
        public List<Project> Projects { get; set; } = new();

        [BindProperty]
        public int? SelectedTeamId { get; set; }
        public IEnumerable<SelectListItem> AvailableTeams { get; set; } = new List<SelectListItem>();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            WorkPackage = await _workPackageService.GetWorkPackageByIdAsync(id);
            Projects = await _projectService.GetAllProjectsAsync();
            if (WorkPackage == null)
            {
                return RedirectToPage("Index");
            }

            var teams = await _workPackageService.GetAvailableTeamsAsync(WorkPackage.ProjectId);
            AvailableTeams = teams.Select(t => new SelectListItem

            {
                Value = t.Id.ToString(),
                Text = t.Name
            });
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _workPackageService.UpdateWorkPackageAsync(WorkPackage);
            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostAssignTeamAsync(int workPackageId)
        {
            if (!SelectedTeamId.HasValue)
            {
                TempData["ErrorMessage"] = "Please select a team.";
                return RedirectToPage();
            }

            await _workPackageService.AssignTeamToWorkPackageAsync(workPackageId, SelectedTeamId.Value);

            return RedirectToPage(new { id = workPackageId });
        }
    }
}
