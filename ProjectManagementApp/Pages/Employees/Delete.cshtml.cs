using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectManagementApp.Services;
using ProjectManagementEntities.Models;

namespace ProjectManagementApp.Pages.Employees
{
    public class DeleteModel : PageModel
    {
        private readonly EmployeeService _employeeService;

        public DeleteModel(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return RedirectToPage("Index");
        }
    }
}
