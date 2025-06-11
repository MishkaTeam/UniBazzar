using Application.Aggregates.Branches;
using Application.Aggregates.Branches.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.Branch
{
    public class IndexModel(BranchesApplication branchesApplication) : PageModel
    {
        public List<UpdateBranchViewModel> ViewModel { get; set; }
        public async Task OnGet()
        {
            ViewModel = await branchesApplication.GetAllBranchesAsync();
        }
    }
}
