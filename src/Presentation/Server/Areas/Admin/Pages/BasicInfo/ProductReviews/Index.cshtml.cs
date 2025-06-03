using Application.Aggregates.Branches.ViewModels;
using Application.Aggregates.Branches;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Aggregates.ProductReviews;
using Application.Aggregates.ProductReviews.ViewModels;

namespace Server.Areas.Admin.Pages.BasicInfo.ProductReviews;

public class IndexModel(ProductReviewApplication commentApplication) : PageModel
{
    public List<UpdateProductReviewViewModel> ViewModel { get; set; }
    public async Task OnGet()
    {
        ViewModel = await commentApplication.GetAllCommentsAsync();
    }
}
