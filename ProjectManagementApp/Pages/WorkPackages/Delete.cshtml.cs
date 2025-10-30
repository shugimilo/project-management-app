using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectManagementApp.Services;
using ProjectManagementEntities.Models;

namespace ProjectManagementApp.Pages.WorkPackages
{
    public class DeleteModel : PageModel
    {
        private readonly WorkPackageService _workPackageService;

        public DeleteModel(WorkPackageService workPackageService)
        {
            _workPackageService = workPackageService;
        }

        [BindProperty]
        public WorkPackage? WorkPackage { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            WorkPackage = await _workPackageService.GetWorkPackageByIdAsync(id);
            if (WorkPackage == null)
            {
                return RedirectToPage("Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _workPackageService.DeleteWorkPackageAsync(id);
            return RedirectToPage("Index");
        }
    }
}
