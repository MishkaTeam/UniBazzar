using Application.Aggregates.Customers;
using Application.Aggregates.ProductReviews;
using Application.Aggregates.ProductReviews.ViewModels;
using Application.Aggregates.Products;
using Application.Aggregates.Products.ProductFeatures;
using Application.Aggregates.Products.ProductImages;
using Application.Aggregates.Products.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Pages;

public class DetailModel(ProductsApplication productsApplication,
                          ProductImagesApplication productImagesApplication,
                          ProductFeaturesApplication productFeaturesApplication,
                          ProductReviewApplication productReviewApplication,
                          CustomerApplication customerApplication) : PageModel
{

    public ProductDetailViewModel ProductDetail { get; set; } = new();

    [BindProperty]
    public CreateProductReviewViewModel CreateCommentViewModel { get; set; } = new();

    public List<DetailsProductReviewViewModel> ViewModel { get; set; }
    public async Task<IActionResult> OnGetAsync(string sku, string slug)
    {
        if (string.IsNullOrWhiteSpace(sku))
        {
            return RedirectToPage("Error/Error404");
        }

        ProductDetail = await productsApplication.GetProductDetails(sku);

        CreateCommentViewModel.ProductId = ProductDetail.Id;

        ViewModel = await productReviewApplication.GetProductReviewsByProductSkuAsync(sku);

        ViewModel = ViewModel.Where(x => x.IsVerified).ToList();

        return Page();
    }

    public async Task<IActionResult> OnPostCommentAsync(string sku, string slug)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToPage("/Account/Login");
        }
        if (!ModelState.IsValid)
        {
            return Page();
        }

        CreateCommentViewModel.CustomerId = Guid.Parse("aa46fc7f-11ba-41e6-886e-18a80142819e");

        CreateCommentViewModel.IsVerified = false;

        await productReviewApplication.Create(CreateCommentViewModel);

        CreateCommentViewModel = new();

        return RedirectToPage("Details", new { sku = sku, slug = slug });
    }
}
