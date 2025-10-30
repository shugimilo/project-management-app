using Microsoft.EntityFrameworkCore;
using ProjectManagementDatabase;
using ProjectManagementEntities.Models;

namespace ProjectManagementApp.Services
{
    public class TaskItemService
    {
        private readonly ProjectManagementContext _context;
        public TaskItemService(ProjectManagementContext context)
        {
            _context = context;
        }
        public async Task<List<TaskItem>> GetAllTasksAsync()
        {
            return await _context.TaskItems
                .Include(t => t.WorkPackage)
                    .ThenInclude(wp => wp.Project)
                .Include(t => t.Activities)
                    .Include(a => a.TimesheetEntries)
                .ToListAsync();
        }
        public async Task<TaskItem?> GetTaskByIdAsync(int taskId)
        {
            return await _context.TaskItems
                .Include(t => t.WorkPackage)
                    .ThenInclude(wp => wp.Project)
                .Include(t => t.Activities)
                .Include(t => t.TaskAssignments)
                    .ThenInclude(ta => ta.Employee)
                .FirstOrDefaultAsync(t => t.Id == taskId);
        }
        public async Task CreateTaskAsync(TaskItem task)
        {
            _context.TaskItems.Add(task);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateTaskAsync(TaskItem updatedTask)
        {
            var existingTask = await _context.TaskItems
                .FirstOrDefaultAsync(t => t.Id == updatedTask.Id);

            if (existingTask == null)
                throw new InvalidOperationException("Task not found");

            // Update only the properties you want
            existingTask.Name = updatedTask.Name;
            existingTask.Description = updatedTask.Description;
            existingTask.Status = updatedTask.Status;
            existingTask.WorkPackageId = updatedTask.WorkPackageId;

            await _context.SaveChangesAsync();
        }
        public async Task DeleteTaskAsync(int taskId)
        {
            var task = await _context.TaskItems.FindAsync(taskId);
            if (task != null)
            {
                _context.TaskItems.Remove(task);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<TaskItem?> GetTaskByIdWithWorkPackageAsync(int taskId)
        {
            return await _context.TaskItems
                .Include(t => t.WorkPackage)
                    .ThenInclude(wp => wp.Project)
                .Include(t => t.Activities)
                    .ThenInclude(a => a.TimesheetEntries)
                .Include(t => t.TaskAssignments)
                    .ThenInclude(ta => ta.Employee)
                .FirstOrDefaultAsync(t => t.Id == taskId);
        }

        public async Task AssignEmployeeToTaskAsync(int taskId, int employeeId, string roleOnTask, decimal plannedHours)
        {
            var task = await _context.TaskItems
                .Include(t => t.TaskAssignments)
                .FirstOrDefaultAsync(t => t.Id == taskId);

            if (task == null)
                throw new InvalidOperationException("Task not found");

            if (task.TaskAssignments.Any(ta => ta.EmployeeId == employeeId))
                throw new InvalidOperationException("Employee is already assigned to this task");

            var assignment = new TaskAssignment
            {
                TaskItemId = taskId,
                EmployeeId = employeeId,
                RoleOnTask = roleOnTask,
                AssignedOn = DateTime.UtcNow,
                Status = AssignmentStatus.Assigned
            };

            _context.TaskAssignments.Add(assignment);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Employee>> GetAvailableEmployeesForTaskAsync(int taskId)
        {
            var task = await _context.TaskItems
                .Include(t => t.WorkPackage)
                .ThenInclude(wp => wp.Project)
                .ThenInclude(p => p.ProjectTeams)
                .FirstOrDefaultAsync(t => t.Id == taskId);

            if (task == null)
                return new List<Employee>();

            var projectTeamIds = task.WorkPackage.Project.ProjectTeams
                .Select(pt => pt.TeamId)
                .ToList();

            var assignedEmployeeIds = await _context.TaskAssignments
                .Where(ta => ta.TaskItemId == taskId)
                .Select(ta => ta.EmployeeId)
                .ToListAsync();

            var availableEmployees = await _context.TeamMembers
                .Where(tm => projectTeamIds.Contains(tm.TeamId))
                .Select(tm => tm.Employee)
                .Where(e => !assignedEmployeeIds.Contains(e.Id))
                .Distinct()
                .ToListAsync();

            return availableEmployees;
        }

        public async Task RemoveEmployeeFromTaskAsync(int taskId, int employeeId)
        {
            var assignment = await _context.TaskAssignments
                .FirstOrDefaultAsync(a => a.TaskItemId == taskId && a.EmployeeId == employeeId);
            if (assignment != null)
            {
                _context.TaskAssignments.Remove(assignment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
