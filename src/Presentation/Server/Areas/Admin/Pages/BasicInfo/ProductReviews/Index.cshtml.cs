using Application.Aggregates.Branches.ViewModels;
using Application.Aggregates.Branches;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Aggregates.ProductReviews;
using Application.Aggregates.ProductReviews.ViewModels;

namespace Server.Areas.Admin.Pages.BasicInfo.ProductReviews;

public class IndexModel(ProductReviewApplication commentApplication) : PageModel
{
    
    public List<DetailsProductReviewViewModel> ViewModel { get; set; } = new();

    public async Task OnGet()
    {
        ViewModel = await commentApplication.GetAllCommentsAsync();
    }
    //public async Task<IActionResult> OnPostApproveAsync()
    //{
    //    var result = await commentApplication.UpdateAsync();
    //    if (!result)
    //        return NotFound();

    //    return RedirectToPage();
    //}
}
