using Application.Aggregates.Customers;
using Application.Aggregates.ProductReviews;
using Application.Aggregates.ProductReviews.ViewModels;
using Application.Aggregates.Products;
using Application.Aggregates.Products.Basket;
using Application.Aggregates.Products.ProductFeatures;
using Application.Aggregates.Products.ProductImages;
using Application.Aggregates.Products.ViewModels;
using BuildingBlocks.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Server.Infrastructure.Extentions;

namespace Server.Pages;

public class DetailModel(ProductsApplication productsApplication,
                          ProductImagesApplication productImagesApplication,
                          ProductFeaturesApplication productFeaturesApplication,
                          ProductReviewApplication productReviewApplication,
                          CustomerApplication customerApplication,
                          IExecutionContextAccessor executionContextAccessor) : PageModel
{

    public ProductDetailViewModel ProductDetail { get; set; } = new();

    [BindProperty]
    public CreateProductReviewViewModel CreateCommentViewModel { get; set; } = new();

    [BindProperty]
    public AddBasketProductViewModel BasketProduct { get; set; } = new();

    public List<DetailsProductReviewViewModel> ViewModel { get; set; }
    public async Task<IActionResult> OnGetAsync(string sku, string slug)
    {
        if (string.IsNullOrWhiteSpace(sku))
        {
            return RedirectToPage("Error/Error404");
        }

        ProductDetail = await productsApplication.GetProductDetails(sku);

        ProductDetail.ProductAttributes.ForEach(x => BasketProduct.SelectedAttributes.Add(x.Name, Guid.Empty));
        BasketProduct.ProductId = ProductDetail.Id;

        CreateCommentViewModel.ProductId = ProductDetail.Id;

        ViewModel = await productReviewApplication.GetProductReviewsByProductSkuAsync(sku);

        ViewModel = ViewModel.Where(x => x.IsVerified).ToList();

        return Page();
    }

    public async Task<IActionResult> OnPostCommentAsync(string sku, string slug)
    {
        if (User.Identity == null
           || User.Identity.IsAuthenticated == false
           || executionContextAccessor.UserId is null)
        {
            return RedirectToRoute("/Account/Login", new { returnUrl = HttpContext.GetUrl() });
        }
        if (!ModelState.IsValid)
        {
            return Page();
        }

        CreateCommentViewModel.CustomerId = executionContextAccessor.UserId!.Value;

        CreateCommentViewModel.IsVerified = false;

        await productReviewApplication.Create(CreateCommentViewModel);

        CreateCommentViewModel = new();

        return RedirectToPage("Details", new { sku = sku, slug = slug });
    }

    public async Task<IActionResult> OnPostPurchaseAsync()
    {

        return Page();

    }
}
