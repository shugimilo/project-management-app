using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectManagementApp.Services;
using ProjectManagementEntities.Models;

namespace ProjectManagementApp.Pages.Employees
{
    public class DetailsModel : PageModel
    {
        private readonly EmployeeService _employeeService;
        public DetailsModel(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        public Employee? Employee { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (Employee == null)
            {
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
