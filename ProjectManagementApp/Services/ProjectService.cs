using Microsoft.EntityFrameworkCore;
using ProjectManagementDatabase;
using ProjectManagementEntities.Models;
namespace ProjectManagementApp.Services

{
    public class ProjectService
    {
        private readonly ProjectManagementContext _context;

        public ProjectService(ProjectManagementContext context)
        {
            _context = context;
        }

        public async Task<List<Project>> GetAllProjectsAsync()
        {
            return await _context.Projects
                .Include(p => p.WorkPackages)
                .Include(p => p.ProjectTeams)
                    .ThenInclude(pt => pt.Team)
                        .ThenInclude(t => t.TeamMembers)
                            .ThenInclude(tm => tm.Employee)
                .ToListAsync();
        }

        public async Task<Project?> GetProjectByIdAsync(int projectId)
        {
            return await _context.Projects
                .Include(p => p.WorkPackages)
                    .ThenInclude(wp => wp.Tasks)
                        .ThenInclude(t => t.Activities)
                            .ThenInclude(a => a.TimesheetEntries)
                .Include(p => p.ProjectTeams)
                    .ThenInclude(pt => pt.Team)
                        .ThenInclude(t => t.TeamMembers)
                            .ThenInclude(tm => tm.Employee)
                .FirstOrDefaultAsync(p => p.Id == projectId);
        }

        public async Task CreateProjectAsync(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProjectAsync(Project project)
        {
            var existingProject = await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == project.Id);
            if (existingProject == null)
                throw new InvalidOperationException("Project not found");
            existingProject.Name = project.Name;
            existingProject.Description = project.Description;
            existingProject.StartDate = project.StartDate;
            existingProject.EndDate = project.EndDate;
            existingProject.Status = project.Status;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProjectAsync(int projectId)
        {
            var project = await _context.Projects.FindAsync(projectId);
            if (project != null)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Employee>> GetEmployeesForProjectAsync(int projectId)
        {
            return await _context.ProjectTeams
                .Where(pt => pt.ProjectId == projectId)
                .Include(pt => pt.Team)
                    .ThenInclude(t => t.TeamMembers)
                        .ThenInclude(tm => tm.Employee)
                .SelectMany(pt => pt.Team.TeamMembers.Select(tm => tm.Employee))
                .Distinct()
                .ToListAsync();
        }

        public async Task<List<Team>> GetAvailableTeamsForProjectAsync(int projectId)
        {
            var assignedTeamIds = await _context.ProjectTeams
                .Where(pt => pt.ProjectId == projectId)
                .Select(pt => pt.TeamId)
                .ToListAsync();
            return await _context.Teams
                .Where(t => !assignedTeamIds.Contains(t.Id))
                .ToListAsync();
        }
        public async Task AssignTeamToProjectAsync(int projectId, int teamId)
        {
            var projectTeam = new ProjectTeam
            {
                ProjectId = projectId,
                TeamId = teamId
            };
            _context.ProjectTeams.Add(projectTeam);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveTeamFromProjectAsync(int projectId, int teamId)
        {
            var projectTeam = await _context.ProjectTeams
                .FirstOrDefaultAsync(pt => pt.ProjectId == projectId && pt.TeamId == teamId);
            if (projectTeam != null)
            {
                _context.ProjectTeams.Remove(projectTeam);
                await _context.SaveChangesAsync();
            }
        }
    }
}
