using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectManagementApp.Services;
using ProjectManagementEntities.Models;

namespace ProjectManagementApp.Pages.Teams
{
    public class EditModel : PageModel
    {
        private readonly TeamService _teamService;
        private readonly EmployeeService _employeeService;

        public EditModel(TeamService teamService, EmployeeService employeeService)
        {
            _teamService = teamService;
            _employeeService = employeeService;
        }

        [BindProperty]
        public Team? Team { get; set; }

        // Employees selected from the "Add Employees" list
        [BindProperty]
        public List<int> SelectedEmployeeIds { get; set; } = new();

        // Lists for Razor page rendering
        public List<Employee> AssignedEmployees { get; set; } = new();
        public List<Employee> EmployeesWithoutTeam { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Team = await _teamService.GetTeamByIdAsync(id);
            if (Team == null) return RedirectToPage("Index");

            // Current team members
            AssignedEmployees = Team.TeamMembers?.Select(tm => tm.Employee).ToList() ?? new List<Employee>();

            // Employees without a team (available to add)
            EmployeesWithoutTeam = await _employeeService.GetEmployeesWithoutTeamAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Team == null) return Page();

            // Update team info
            await _teamService.UpdateTeamAsync(Team);

            return RedirectToPage(new { id = Team.Id });
        }

        public async Task<IActionResult> OnPostAddEmployeesAsync(int teamId)
        {
            if (SelectedEmployeeIds.Count == 0) return RedirectToPage(new { id = teamId });

            foreach (var employeeId in SelectedEmployeeIds)
            {
                await _employeeService.UpdateEmployeeTeamAsync(employeeId, teamId);
            }

            return RedirectToPage(new { id = teamId });
        }

        public async Task<IActionResult> OnPostRemoveEmployeeAsync(int employeeId)
        {
            if (Team == null) return RedirectToPage("Index");

            // Remove employee from team
            await _employeeService.UpdateEmployeeTeamAsync(employeeId, null);

            return RedirectToPage(new { id = Team.Id });
        }
    }
}
