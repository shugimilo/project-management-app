using Microsoft.EntityFrameworkCore;
using ProjectManagementDatabase;
using ProjectManagementEntities.Models;

namespace ProjectManagementApp.Services
{
    public class WorkPackageService
    {
        private readonly ProjectManagementContext _context;
        private readonly ProjectTeamService _projectTeamService = null!;
        public WorkPackageService(ProjectManagementContext context, ProjectTeamService projectTeamService)
        {
            _context = context;
            _projectTeamService = projectTeamService;
        }
        public async Task<List<WorkPackage>> GetAllWorkPackagesAsync()
        {
            return await _context.WorkPackages
                .Include(wp => wp.Project)
                .Include(wp => wp.Tasks)
                .ToListAsync();
        }
        public async Task<WorkPackage?> GetWorkPackageByIdAsync(int workPackageId)
        {
            return await _context.WorkPackages
                .Include(wp => wp.Project)
                .Include(wp => wp.Team)
                .Include(wp => wp.Tasks)
                    .ThenInclude(t => t.Activities)
                        .ThenInclude(a => a.TimesheetEntries)
                .Include(wp => wp.Tasks)
                    .ThenInclude(t => t.TaskAssignments)
                        .ThenInclude(ta => ta.Employee)
                .FirstOrDefaultAsync(wp => wp.Id == workPackageId);
        }
        public async Task CreateWorkPackageAsync(WorkPackage workPackage)
        {
            _context.WorkPackages.Add(workPackage);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateWorkPackageAsync(WorkPackage workPackage)
        {
            var existingWorkPackage = await _context.WorkPackages
                .FirstOrDefaultAsync(wp => wp.Id == workPackage.Id);
            if (existingWorkPackage == null)
                throw new InvalidOperationException("Work Package not found");
            existingWorkPackage.Name = workPackage.Name;
            existingWorkPackage.Description = workPackage.Description;
            existingWorkPackage.ProjectId = workPackage.ProjectId;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteWorkPackageAsync(int workPackageId)
        {
            var workPackage = await _context.WorkPackages.FindAsync(workPackageId);
            if (workPackage != null)
            {
                _context.WorkPackages.Remove(workPackage);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Team>> GetAvailableTeamsAsync(int projectId)
        {
            return await _projectTeamService.GetTeamsByProjectIdAsync(projectId);
        }

        public async Task AssignTeamToWorkPackageAsync(int workPackageId, int newTeamId)
        {
            var workPackage = await _context.WorkPackages
                .Include(wp => wp.Project)
                .Include(wp => wp.Tasks)
                    .ThenInclude(t => t.TaskAssignments)
                        .ThenInclude(ta => ta.Employee)
                .FirstOrDefaultAsync(wp => wp.Id == workPackageId);

            if (workPackage == null)
                throw new InvalidOperationException("Work Package not found");

            var teamInProject = await _context.ProjectTeams
                .AnyAsync(pt => pt.ProjectId == workPackage.ProjectId && pt.TeamId == newTeamId);

            if (!teamInProject)
                throw new InvalidOperationException("Team is not part of this project");

            workPackage.TeamId = newTeamId;

            foreach (var task in workPackage.Tasks)
            {
                var toRemove = task.TaskAssignments
                    .Where(ta => ta.Employee.TeamMembership?.TeamId != newTeamId)
                    .ToList();

                if (toRemove.Any())
                    _context.TaskAssignments.RemoveRange(toRemove);
            }

            await _context.SaveChangesAsync();
        }


        public async Task<List<Employee>> GetTeamMembersAsync(int workPackageId)
        {
            var teamId = await _context.WorkPackages
                .Where(wp => wp.Id == workPackageId)
                .Select(wp => wp.TeamId)
                .FirstOrDefaultAsync();

            if (teamId == 0)
                return new List<Employee>();

            return await _context.TeamMembers
                .Where(tm => tm.TeamId == teamId)
                .Include(tm => tm.Employee)
                .Select(tm => tm.Employee)
                .ToListAsync();
        }
    }
}
