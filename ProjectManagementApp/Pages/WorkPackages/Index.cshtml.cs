using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectManagementApp.Services;
using ProjectManagementEntities.Models;

namespace ProjectManagementApp.Pages.WorkPackages
{
    public class IndexModel : PageModel
    {
        private readonly WorkPackageService _workPackageService;

        public IndexModel(WorkPackageService workPackageService)
        {
            _workPackageService = workPackageService;
        }

        public List<WorkPackage> WorkPackages { get; set; } = new();
        public async Task OnGetAsync()
        {
            WorkPackages = await _workPackageService.GetAllWorkPackagesAsync();
        }
    }
}
