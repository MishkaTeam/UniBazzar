using Application.Aggregates.Products;
using Application.Aggregates.Products.ProductFeatures.ViewModels;
using Application.Aggregates.Products.ProductImages.ViewModel;
using Application.Aggregates.Products.ProductPriceLists.ViewModels;
using Application.Aggregates.Products.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Pages;

public class DetailModel(ProductsApplication productsApplication,
                          ProductImagesApplication productImagesApplication,
                          ProductFeaturesApplication productFeaturesApplication,
                          ProductPriceListsApplication productPriceListsApplication) : PageModel
{

    public ProductViewModel ViewModelProduct { get; set; }
    public List<ProductImageViewModel> ViewModelProductImage { get; set; }
    public List<ProductFeatureViewModel> ViewModelProductFeature { get; set; }
    public ProductPriceListViewModel ViewModelProductPrice { get; set; }

    public async Task<IActionResult> OnGetAsync(string sku, string slug)
    {
        if (string.IsNullOrWhiteSpace(sku))
        {
            return RedirectToPage("Error/Error404");
        }
    
        ViewModelProduct = await productsApplication.GetProductAsync(sku);

        ViewModelProductImage = await productImagesApplication.GetImageByProductIdAsync(ViewModelProduct.Id);

        ViewModelProductFeature = await productFeaturesApplication.GetProductFeatures(ViewModelProduct.Id);

        ViewModelProductPrice = await productPriceListsApplication.GetPriceByProductId(ViewModelProduct.Id);

        return Page();
    }
}
