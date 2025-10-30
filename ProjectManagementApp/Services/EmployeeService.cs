using Microsoft.EntityFrameworkCore;
using ProjectManagementDatabase;
using ProjectManagementEntities.Models;

namespace ProjectManagementApp.Services
{
    public class EmployeeService
    {
        private readonly ProjectManagementContext _context;
        public EmployeeService(ProjectManagementContext context)
        {
            _context = context;
        }
        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees
                .Include(e => e.TeamMembership)
                    .ThenInclude(tm => tm.Team)
                .Include(e => e.TaskAssignments)
                    .ThenInclude(ta => ta.TaskItem)
                        .ThenInclude(ti => ti.WorkPackage)
                .Include(e => e.TimesheetEntries)
                    .ThenInclude(te => te.Activity)
                .ToListAsync();
        }
        public async Task<Employee?> GetEmployeeByIdAsync(int employeeId)
        {
            return await _context.Employees
                .Include(e => e.TeamMembership)
                    .ThenInclude(tm => tm.Team)
                .Include(e => e.TaskAssignments)
                    .ThenInclude(ta => ta.TaskItem)
                        .ThenInclude(ti => ti.WorkPackage)
                .Include(e => e.TimesheetEntries)
                    .ThenInclude(te => te.Activity)
                        .ThenInclude(a => a.TaskItem)
                            .ThenInclude(ta => ta.WorkPackage)
                                .ThenInclude(wp => wp.Project)
                .FirstOrDefaultAsync(e => e.Id == employeeId);
        }
        public async Task CreateEmployeeAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateEmployeeAsync(Employee employee)
        {
            var existingEmployee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == employee.Id);
            if (existingEmployee == null)
                throw new InvalidOperationException("Employee not found");
            existingEmployee.FirstName = employee.FirstName;
            existingEmployee.LastName = employee.LastName;
            existingEmployee.Email = employee.Email;
            existingEmployee.HireDate = employee.HireDate;
            existingEmployee.IsActive = employee.IsActive;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteEmployeeAsync(int employeeId)
        {
            var employee = await _context.Employees.FindAsync(employeeId);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateEmployeeTeamAsync(int employeeId, int? teamId)
        {
            var employee = await _context.Employees
                .Include(e => e.TeamMembership)
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee == null)
                throw new ArgumentException("Employee not found");

            // Remove membership if exists
            if (employee.TeamMembership != null)
            {
                _context.TeamMembers.Remove(employee.TeamMembership);
                employee.TeamMembership = null;
            }

            // Assign new team if teamId is provided
            if (teamId.HasValue)
            {
                var teamMember = new TeamMember
                {
                    EmployeeId = employeeId,
                    TeamId = teamId.Value
                };
                employee.TeamMembership = teamMember;
                _context.TeamMembers.Add(teamMember);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<Employee>> GetEmployeesWithoutTeamAsync()
        {
            return await _context.Employees
                .Where(e => e.TeamMembership == null)
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToListAsync();
        }


    }
}
