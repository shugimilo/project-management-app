using Microsoft.EntityFrameworkCore;
using ProjectManagementDatabase;
using ProjectManagementEntities.Models;

namespace ProjectManagementApp.Services
{
    public class ProjectTeamService
    {
        private readonly ProjectManagementContext _context;
        public ProjectTeamService(ProjectManagementContext context)
        {
            _context = context;
        }
        public async Task<List<ProjectTeam>> GetAllProjectTeamsAsync()
        {
            return await _context.ProjectTeams
                .Include(pt => pt.Project)
                    .ThenInclude(p => p.WorkPackages)
                .Include(pt => pt.Team)
                    .ThenInclude(t => t.TeamMembers)
                        .ThenInclude(tm => tm.Employee)
                .ToListAsync();
        }
        public async Task<ProjectTeam?> GetProjectTeamByIdAsync(int projectId, int teamId)
        {
            return await _context.ProjectTeams
                .Include(pt => pt.Project)
                    .ThenInclude(pt => pt.WorkPackages)
                .Include(pt => pt.Team)
                    .ThenInclude(t => t.TeamMembers)
                        .ThenInclude(tm => tm.Employee)
                .FirstOrDefaultAsync(pt => pt.ProjectId == projectId && pt.TeamId == teamId);
        }
        public async Task<List<Team>> GetTeamsByProjectIdAsync(int projectId)
        {
            return await _context.ProjectTeams
                .Where(pt => pt.ProjectId == projectId)
                .Include(pt => pt.Team)
                .Select(pt => pt.Team)
                .ToListAsync();
        }
        public async Task CreateProjectTeamAsync(ProjectTeam projectTeam)
        {
            _context.ProjectTeams.Add(projectTeam);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateProjectTeamAsync(ProjectTeam projectTeam)
        {
            _context.ProjectTeams.Update(projectTeam);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteProjectTeamAsync(int projectTeamId)
        {
            var projectTeam = await _context.ProjectTeams.FindAsync(projectTeamId);
            if (projectTeam != null)
            {
                _context.ProjectTeams.Remove(projectTeam);
                await _context.SaveChangesAsync();
            }
        }
    }
}
