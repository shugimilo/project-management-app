using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectManagementApp.Services;
using ProjectManagementEntities.Models;

namespace ProjectManagementApp.Pages.Teams
{
    public class DeleteModel : PageModel
    {
        private readonly TeamService _teamService;

        public DeleteModel(TeamService teamService)
        {
            _teamService = teamService;
        }

        [BindProperty]
        public Team? Team { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Team = await _teamService.GetTeamByIdAsync(id);
            if (Team == null)
            {
                return RedirectToPage("Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _teamService.DeleteTeamAsync(id);

            return RedirectToPage("Index");
        }
    }
}
