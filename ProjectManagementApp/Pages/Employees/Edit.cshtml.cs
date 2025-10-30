using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectManagementApp.Services;
using ProjectManagementEntities.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjectManagementApp.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly EmployeeService _employeeService;
        private readonly TeamService _teamService;
        public EditModel(EmployeeService employeeService, TeamService teamService)
        {
            _employeeService = employeeService;
            _teamService = teamService;
        }
        [BindProperty]
        public Employee? Employee { get; set; } = null!;

        public List<SelectListItem> IsActiveOptions { get; } = new()
        {
            new SelectListItem { Value = "true", Text = "Active" },
            new SelectListItem { Value = "false", Text = "Inactive" }
        };

        [BindProperty]
        public int? SelectedTeamId { get; set; }
        public IEnumerable<SelectListItem> AvailableTeams { get; set; } = new List<SelectListItem>();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (Employee == null)
            {
                return RedirectToPage("Index");
            }

            var teams = await _teamService.GetAllTeamsAsync();

            AvailableTeams = teams.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Name,
            }).ToList();

            SelectedTeamId = Employee.TeamMembership?.TeamId;

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var teams = await _teamService.GetAllTeamsAsync();
                AvailableTeams = teams.Select(t => new SelectListItem 
                { 
                    Value= t.Id.ToString(),
                    Text = t.Name
                }).ToList();
                return Page();
            }
            await _employeeService.UpdateEmployeeAsync(Employee);

            await _employeeService.UpdateEmployeeTeamAsync(Employee.Id, SelectedTeamId);

            return RedirectToPage("Index");
        }
    }
}
