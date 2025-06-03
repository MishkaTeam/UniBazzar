using Application.Aggregates.ProductReviews;
using Application.Aggregates.ProductReviews.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.ProductReviews
{
    public class UpdateModel(ProductReviewApplication commentApplication) : PageModel
    {
        [BindProperty]
        public UpdateProductReviewViewModel UpdateViewModel { get; set; } = new();
        public async Task OnGet(Guid Id)
        {
            UpdateViewModel = await commentApplication.GetCommentAsync(Id);
        }
        public async Task<IActionResult> OnPost()
        {
            await commentApplication.UpdateAsync(UpdateViewModel);
            return RedirectToPage("Index");
        }
    }
}
