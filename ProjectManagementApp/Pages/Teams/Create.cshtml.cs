using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectManagementApp.Services;
using ProjectManagementEntities.Models;

namespace ProjectManagementApp.Pages.Teams
{
    public class CreateModel : PageModel
    {
        private readonly TeamService _teamService;

        public CreateModel(TeamService teamService)
        {
            _teamService = teamService;
        }

        [BindProperty]
        public Team Team { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _teamService.CreateTeamAsync(Team);
            return RedirectToPage("Index");
        }
    }
}
