using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectManagementApp.Services;
using ProjectManagementEntities.Models;

namespace ProjectManagementApp.Pages.Projects
{
    public class EditModel : PageModel
    {
        private readonly ProjectService _projectService;

        public EditModel(ProjectService projectService)
        {
            _projectService = projectService;
        }

        [BindProperty]
        public Project? Project { get; set; } = null!;

        [BindProperty]
        public int? SelectedTeamId { get; set; }
        public IEnumerable<SelectListItem> TeamOptions { get; set; } = new List<SelectListItem>();
        public List<Team> AvailableTeams { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Project = await _projectService.GetProjectByIdAsync(id);
            if (Project == null)
            {
                return RedirectToPage("Index");
            }
            AvailableTeams = await _projectService.GetAvailableTeamsForProjectAsync(id);
            TeamOptions = AvailableTeams.Select(t => new SelectListItem
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
            await _projectService.UpdateProjectAsync(Project);
            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostAssignTeamAsync(int projectId)
        {
            if (!SelectedTeamId.HasValue)
            {
                TempData["ErrorMessage"] = "Please select a team.";
                return RedirectToPage();
            }

            await _projectService.AssignTeamToProjectAsync(projectId, SelectedTeamId.Value);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRemoveTeamAsync(int projectId, int teamId)
        {
            await _projectService.RemoveTeamFromProjectAsync(projectId, teamId);

            Project = await _projectService.GetProjectByIdAsync(projectId);
            AvailableTeams = await _projectService.GetAvailableTeamsForProjectAsync(projectId);
            TeamOptions = AvailableTeams.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Name
            });

            return Page();
        }
    }
}
