using Application.Aggregates.Branches;
using Application.Aggregates.Branches.ViewModels;
using Application.Aggregates.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.Branch
{
    public class CreateModel(BranchesApplication branchesApplication) : PageModel
    {
        [BindProperty]
        public CreateBranchViewModel BranchViewModel { get; set; } = new();

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await branchesApplication.CreateAsync(BranchViewModel);
            }
            return RedirectToPage("Index");
        }
    }
}
