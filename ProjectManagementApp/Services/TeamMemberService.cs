using Microsoft.EntityFrameworkCore;
using ProjectManagementDatabase;
using ProjectManagementEntities.Models;

namespace ProjectManagementApp.Services
{
    public class TeamMemberService
    {
        private readonly ProjectManagementContext _context;

        public TeamMemberService(ProjectManagementContext context)
        {
            _context = context;
        }

        public async Task<List<TeamMember>> GetAllTeamMembersAsync()
        {
            return await _context.TeamMembers
                .Include(tm => tm.Team)
                .Include(tm => tm.Employee)
                .ToListAsync();
        }

        public async Task<TeamMember?> GetTeamMemberAsync(int teamId, int employeeId)
        {
            return await _context.TeamMembers
                .Include(tm => tm.Team)
                .Include(tm => tm.Employee)
                .FirstOrDefaultAsync(tm => tm.TeamId == teamId && tm.EmployeeId == employeeId);
        }

        public async Task CreateTeamMemberAsync(TeamMember teamMember)
        {
            _context.TeamMembers.Add(teamMember);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTeamMemberAsync(TeamMember teamMember)
        {
            _context.TeamMembers.Update(teamMember);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTeamMemberAsync(int teamId, int employeeId)
        {
            var teamMember = await _context.TeamMembers
                .FirstOrDefaultAsync(tm => tm.TeamId == teamId && tm.EmployeeId == employeeId);
            if (teamMember != null)
            {
                _context.TeamMembers.Remove(teamMember);
                await _context.SaveChangesAsync();
            }
        }
    }
}
