using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectManagementApp.Services;
using ProjectManagementEntities.Models;

namespace ProjectManagementApp.Pages.Projects
{
    public class DetailsModel : PageModel
    {
        private readonly ProjectService _projectService;
        private readonly TeamService _teamService;
        private readonly ProjectTeamService _projectTeamService;

        public DetailsModel(ProjectService projectService, TeamService teamService, ProjectTeamService projectTeamService)
        {
            _projectService = projectService;
            _teamService = teamService;
            _projectTeamService = projectTeamService;
        }

        public Project? Project { get; set; }
        public List<SelectListItem> TeamOptions { get; set; } = new();

        [BindProperty]
        public int SelectedTeamId { get; set; }

        public decimal PlannedHours { get; set; }
        public decimal ActualHours { get; set; }
        public decimal ProgressPercent { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Project = await _projectService.GetProjectByIdAsync(id);

            if (Project == null)
            {
                return RedirectToPage("Index");
            }

            PlannedHours = Project?.WorkPackages?.Sum(wp => wp.Tasks.Sum(t => t.Activities.Sum(a => a.PlannedHours))) ?? 0m;
            ActualHours = Project?.WorkPackages?.Sum(wp => wp.Tasks.Sum(t => t.Activities.Sum(a => a.ActualHours))) ?? 0m;
            ProgressPercent = Project.CalculateProgress();

            var teams = await _teamService.GetAllTeamsAsync();
            TeamOptions = teams.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Name
            }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAssignTeamAsync(int projectId, int teamId)
        {
            // Check if assignment already exists
            var existingAssignment = await _projectTeamService.GetProjectTeamByIdAsync(projectId, teamId);

            if (existingAssignment != null)
            {
                TempData["ErrorMessage"] = "This team is already assigned to the project.";
                return RedirectToPage(); // refresh page and show error
            }
            else
            {

                // Create new assignment
                var projectTeam = new ProjectTeam
                {
                    ProjectId = projectId,
                    TeamId = teamId,
                    From = DateOnly.FromDateTime(DateTime.Now)
                };
                await _projectTeamService.CreateProjectTeamAsync(projectTeam);

                return RedirectToPage();
            }
        }

    }
}
