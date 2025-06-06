using Application.Aggregates.Products;
using Application.Aggregates.Products.ProductFeatures.ViewModels;
using Application.Aggregates.Products.ProductImages.ViewModel;
using Application.Aggregates.Products.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Pages;

public class DetailModel(ProductsApplication productsApplication,
                          ProductImagesApplication productImagesApplication,
                          ProductFeaturesApplication productFeaturesApplication) : PageModel
{

    public ProductDetailViewModel ProductDetail { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(string sku, string slug)
    {
        if (string.IsNullOrWhiteSpace(sku))
        {
            return RedirectToPage("Error/Error404");
        }
    
        ProductDetail = await productsApplication.GetProductDetails(sku);

        return Page();
    }
}
