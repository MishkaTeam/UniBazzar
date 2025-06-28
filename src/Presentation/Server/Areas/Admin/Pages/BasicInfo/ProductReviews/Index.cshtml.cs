using Application.Aggregates.ProductReviews;
using Application.Aggregates.ProductReviews.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.ProductReviews;

public class IndexModel(ProductReviewApplication commentApplication) : PageModel
{
    public List<DetailsProductReviewViewModel> ViewModel { get; set; } = new();

    public async Task OnGet()
    {
        ViewModel = await commentApplication.GetAllCommentsAsync();
        ViewModel = ViewModel.OrderByDescending(x => x.InsertDateTime).ToList();
    }
}
