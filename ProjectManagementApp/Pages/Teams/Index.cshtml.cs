using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectManagementApp.Services;
using ProjectManagementEntities.Models;

namespace ProjectManagementApp.Pages.Teams
{
    public class IndexModel : PageModel
    {
        private readonly TeamService _teamService;

        public IndexModel(TeamService teamService)
        {
            _teamService = teamService;
        }

        public List<Team> Teams { get; set; } = new();

        public async Task OnGetAsync()
        {
            Teams = await _teamService.GetAllTeamsAsync();
        }
    }
}
