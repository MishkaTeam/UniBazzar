using Application.Aggregates.Products;
using Application.Aggregates.Products.ProductFeatures.ViewModels;
using Application.Aggregates.Products.ProductImages.ViewModel;
using Application.Aggregates.Products.ProductPriceLists.ViewModels;
using Application.Aggregates.Products.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Pages;

public class ProductModel(ProductsApplication productsApplication,
                          ProductImagesApplication productImagesApplication,
                          ProductFeaturesApplication productFeaturesApplication,
                          ProductPriceListsApplication productPriceListsApplication) : PageModel
{

    public ProductViewModel ViewModelProduct { get; set; }
    public List<ProductImageViewModel> ViewModelProductImage { get; set; }
    public List<ProductFeatureViewModel> ViewModelProductFeature { get; set; }
    public ProductPriceListViewModel ViewModelProductPrice { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id.HasValue == false || id.Value == Guid.Empty)
        {
            return RedirectToPage("Index");
        }

        ViewModelProduct = await productsApplication.GetProductAsync(id.Value);

        ViewModelProductImage = await productImagesApplication.GetImageByProductIdAsync(id.Value);

        ViewModelProductFeature = await productFeaturesApplication.GetProductFeatures(id.Value);

        ViewModelProductPrice = await productPriceListsApplication.GetPriceByProductId(id.Value);

        return Page();
    }
}
