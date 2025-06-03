using Application.Aggregates.Branches.ViewModels;
using Application.Aggregates.Branches;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Aggregates.ProductReviews.ViewModels;
using Application.Aggregates.ProductReviews;

namespace Server.Areas.Admin.Pages.BasicInfo.ProductReviews
{
    public class CreateModel(ProductReviewApplication commentApplication) : PageModel
    {

        [BindProperty]
        public CreateProductReviewViewModel ViewModel { get; set; } = new();

        public async Task OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await commentApplication.Create(ViewModel);
            }
            return RedirectToPage("Index");
        }
    }
}
