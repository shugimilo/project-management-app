using Microsoft.EntityFrameworkCore;
using ProjectManagementDatabase;
using ProjectManagementEntities.Models;

namespace ProjectManagementApp.Services
{
    public class TimesheetEntryService
    {
        private readonly ProjectManagementContext _context;
        public TimesheetEntryService(ProjectManagementContext context)
        {
            _context = context;
        }
        public async Task<List<TimesheetEntry>> GetAllTimesheetEntriesAsync()
        {
            return await _context.TimesheetEntries
                .Include(te => te.Employee)
                .Include(te => te.Activity)
                    .ThenInclude(a => a.TaskItem)
                        .ThenInclude(ti => ti.WorkPackage)
                            .ThenInclude(wp => wp.Project)
                .ToListAsync();
        }
        public async Task<TimesheetEntry?> GetTimesheetEntryByIdAsync(int timesheetEntryId)
        {
            return await _context.TimesheetEntries
                .Include(te => te.Employee)
                .Include(te => te.Activity)
                    .ThenInclude(a => a.TaskItem)
                        .ThenInclude(ta => ta.WorkPackage)
                            .ThenInclude(wp => wp.Project)
                .FirstOrDefaultAsync(te => te.Id == timesheetEntryId);
        }
        public async Task CreateTimesheetEntryAsync(TimesheetEntry timesheetEntry)
        {
            _context.TimesheetEntries.Add(timesheetEntry);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateTimesheetEntryAsync(TimesheetEntry timesheetEntry)
        {
            _context.TimesheetEntries.Update(timesheetEntry);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteTimesheetEntryAsync(int timesheetEntryId)
        {
            var timesheetEntry = await _context.TimesheetEntries
                .FirstOrDefaultAsync(te => te.Id == timesheetEntryId);
            if (timesheetEntry != null)
            {
                _context.TimesheetEntries.Remove(timesheetEntry);
                await _context.SaveChangesAsync();
            }
        }
    }
}
