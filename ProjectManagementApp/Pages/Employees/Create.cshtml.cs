using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectManagementEntities.Models;
using ProjectManagementApp.Services;

namespace ProjectManagementApp.Pages.Employees
{
    public class CreateModel : PageModel
    {
        private readonly EmployeeService _employeeService;
        public CreateModel(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [BindProperty]
        public Employee Employee { get; set; } = new Employee();
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _employeeService.CreateEmployeeAsync(Employee);
            return RedirectToPage("Index");

        }
    }
}
