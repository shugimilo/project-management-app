using Microsoft.EntityFrameworkCore;
using ProjectManagementDatabase;
using ProjectManagementEntities.Models;

namespace ProjectManagementApp.Services
{
    public class TeamService
    {
        private readonly ProjectManagementContext _context;
        public TeamService(ProjectManagementContext context)
        {
            _context = context;
        }
        public async Task<List<Team>> GetAllTeamsAsync()
        {
            return await _context.Teams
                .Include(t => t.TeamMembers)
                    .ThenInclude(tm => tm.Employee)
                .Include(t => t.ProjectTeams)
                    .ThenInclude(pt => pt.Project)
                .ToListAsync();
        }
        public async Task<Team?> GetTeamByIdAsync(int teamId)
        {
            return await _context.Teams
                .Include(t => t.WorkPackages)
                .Include(t => t.TeamMembers)
                    .ThenInclude(tm => tm.Employee)
                        .ThenInclude(e => e.TimesheetEntries)
                            .ThenInclude(tse => tse.Activity)
                                .ThenInclude(a => a.TaskItem)
                                    .ThenInclude(ti => ti.WorkPackage)
                                        .ThenInclude(wp => wp.Project)
                .Include(t => t.ProjectTeams)
                    .ThenInclude(pt => pt.Project)
                        .ThenInclude(p => p.WorkPackages)
                .FirstOrDefaultAsync(t => t.Id == teamId);
        }
        public async Task CreateTeamAsync(Team team)
        {
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateTeamAsync(Team updatedTeam)
        {
            var existingTeam = await _context.Teams
                .FirstOrDefaultAsync(t => t.Id == updatedTeam.Id);
            if (existingTeam == null)
                throw new InvalidOperationException("Team not found");
            existingTeam.Name = updatedTeam.Name;
            existingTeam.Description = updatedTeam.Description;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteTeamAsync(int teamId)
        {
            var team = await _context.Teams.FindAsync(teamId);
            if (team != null)
            {
                _context.Teams.Remove(team);
                await _context.SaveChangesAsync();
            }
        }
    }
}
