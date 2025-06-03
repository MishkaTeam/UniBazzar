using Application.Aggregates.Branches.ViewModels;
using Application.Aggregates.Branches;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Aggregates.ProductReviews;
using Application.Aggregates.ProductReviews.ViewModels;

namespace Server.Areas.Admin.Pages.BasicInfo.ProductReviews
{
    public class DeleteModel(ProductReviewApplication commentApplication) : PageModel
    {

        [BindProperty]
        public UpdateProductReviewViewModel DeleteViewModel { get; set; } = new();
        public async Task OnGet(Guid Id)
        {
            DeleteViewModel = await commentApplication.GetCommentAsync(Id);
        }
        public async Task<IActionResult> OnPost()
        {
            await commentApplication.DeleteAsync(DeleteViewModel.Id);
            return RedirectToPage("Index");
        }
    }
}
