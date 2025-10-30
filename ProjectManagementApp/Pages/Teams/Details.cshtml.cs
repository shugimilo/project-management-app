using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectManagementApp.Services;
using ProjectManagementEntities.Models;

namespace ProjectManagementApp.Pages.Teams
{
    public class DetailsModel : PageModel
    {
        private readonly TeamService _teamService;
        public DetailsModel(TeamService teamService)
        {
            _teamService = teamService;
        }
        public Team? Team { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Team = await _teamService.GetTeamByIdAsync(id);
            if (Team == null)
            {
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
