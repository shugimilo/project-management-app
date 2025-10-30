using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectManagementApp.Services;
using ProjectManagementEntities.Models;

namespace ProjectManagementApp.Pages.WorkPackages
{
    public class DetailsModel : PageModel
    {
        private readonly WorkPackageService _workPackageService;

        public DetailsModel(WorkPackageService workPackageService)
        {
            _workPackageService = workPackageService;
        }

        public WorkPackage? WorkPackage { get; set; } = null!;

        public decimal PlannedHours { get; set; }
        public decimal ActualHours { get; set; }
        public decimal ProgressPercent { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            WorkPackage = await _workPackageService.GetWorkPackageByIdAsync(id);
            if (WorkPackage == null)
            {
                return RedirectToPage("Index");
            }

            PlannedHours = WorkPackage.Tasks.SelectMany(t => t.Activities).Sum(a => a.PlannedHours);
            ActualHours = WorkPackage.Tasks.SelectMany(t => t.Activities).Sum(a => a.ActualHours);
            ProgressPercent = WorkPackage.CalculateProgress();
            return Page();
        }

    }
}
