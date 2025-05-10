using Application.Aggregates.Branches;
using Application.Aggregates.Branches.ViewModels;
using Application.Aggregates.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.Branch
{
    public class UpdateModel(BranchesApplication branchesApplication) : PageModel
    {
        [BindProperty]
        public UpdateBranchViewModel UpdateViewModel { get; set; }=new();
        public async Task OnGet(Guid Id)
        {
            UpdateViewModel = await branchesApplication.GetBranchAsync(Id);
        }
        public async Task<IActionResult> OnPost()
        {
            await branchesApplication.UpdateAsync(UpdateViewModel);
            return RedirectToPage("Index");
        }
    }
}
