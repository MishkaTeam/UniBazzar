using Application.Aggregates.Products;
using Application.Aggregates.Products.ProductFeatures.ViewModels;
using Application.Aggregates.Products.ProductImages.ViewModel;
using Application.Aggregates.Products.ProductPriceLists.ViewModels;
using Application.Aggregates.Products.ViewModels;
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

    public async Task OnGetAsync(Guid id)
    {
        ViewModelProduct = await productsApplication.GetProductAsync(id);

        ViewModelProductImage = await productImagesApplication.GetImageByProductIdAsync(id);

        ViewModelProductFeature = await productFeaturesApplication.GetProductFeatures(id);

        ViewModelProductPrice = await productPriceListsApplication.GetPriceByProductId(id);
    }
}
