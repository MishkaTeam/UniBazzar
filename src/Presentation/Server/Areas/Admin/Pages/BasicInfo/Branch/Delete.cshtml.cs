using Application.Aggregates.Branches;
using Application.Aggregates.Branches.ViewModels;
using Application.Aggregates.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.Branch
{
    public class DeleteModel(BranchesApplication branchesApplication) : PageModel
    {
        [BindProperty]
        public UpdateBranchViewModel DeleteViewModel { get; set; } = new();
        public async Task OnGet(Guid Id)
        {
            DeleteViewModel = await branchesApplication.GetBranchAsync(Id);
        }
        public async Task<IActionResult> OnPost()
        {
            await branchesApplication.DeleteAsync(DeleteViewModel.Id);
            return RedirectToPage("Index");
        }
    }
}
