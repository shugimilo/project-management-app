using Microsoft.EntityFrameworkCore;
using ProjectManagementDatabase;
using ProjectManagementEntities.Models;

namespace ProjectManagementApp.Services
{
    public class TaskAssignmentService
    {
        private readonly ProjectManagementContext _context;

        public TaskAssignmentService(ProjectManagementContext context)
        {
            _context = context;
        }

        public async Task<List<TaskAssignment>> GetAllTaskAssignmentsAsync()
        {
            return await _context.TaskAssignments
                .Include(ta => ta.TaskItem)
                    .ThenInclude(t => t.WorkPackage)
                        .ThenInclude(wp => wp.Project)
                .Include(ta => ta.Employee)
                .ToListAsync();
        }

        public async Task<TaskAssignment?> GetTaskAssignmentByIdAsync(int taskItemId, int employeeId)
        {
            return await _context.TaskAssignments
                .Include(ta => ta.TaskItem)
                    .ThenInclude(t => t.WorkPackage)
                        .ThenInclude(wp => wp.Project)
                .Include(ta => ta.Employee)
                .FirstOrDefaultAsync(ta => ta.TaskItemId == taskItemId && ta.EmployeeId == employeeId);
        }

        public async Task CreateTaskAssignmentAsync(TaskAssignment taskAssignment)
        {
            _context.TaskAssignments.Add(taskAssignment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskAssignmentAsync(TaskAssignment taskAssignment)
        {
            _context.TaskAssignments.Update(taskAssignment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAssignmentAsync(int taskItemId, int employeeId)
        {
            var taskAssignment = await _context.TaskAssignments
                .FirstOrDefaultAsync(ta => ta.TaskItemId == taskItemId && ta.EmployeeId == employeeId);
            if (taskAssignment != null)
            {
                _context.TaskAssignments.Remove(taskAssignment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
