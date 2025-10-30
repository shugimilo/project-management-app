using Microsoft.EntityFrameworkCore;
using ProjectManagementDatabase;
using ProjectManagementEntities.Models;

namespace ProjectManagementApp.Services
{
    public class ActivityService
    {
        private readonly ProjectManagementContext _context;

        public ActivityService(ProjectManagementContext context)
        {
            _context = context;
        }

        public async Task<List<Activity>> GetAllActivitiesAsync()
        {
            return await _context.Activities
                .Include(a => a.TaskItem)
                .ThenInclude(t => t.TimesheetEntries)
                .ToListAsync();
        }

        public async Task<Activity?> GetActivityByIdAsync(int activityId)
        {
            return await _context.Activities
                .Include(a => a.TaskItem)
                .ThenInclude(t => t.TimesheetEntries)
                .FirstOrDefaultAsync(a => a.Id == activityId);
        }

        public async Task CreateActivityAsync(Activity activity)
        {
            _context.Activities.Add(activity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateActivityAsync(Activity activity)
        {
            _context.Activities.Update(activity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteActivityAsync(int activityId)
        {
            var activity = await _context.Activities.FindAsync(activityId);
            if (activity != null)
            {
                _context.Activities.Remove(activity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
