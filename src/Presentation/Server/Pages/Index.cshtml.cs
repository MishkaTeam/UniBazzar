using Application.Aggregates.Categories;
using Application.Aggregates.Categories.ViewModels;
using Application.Aggregates.Products;
using Application.Aggregates.Products.ProductImages.ViewModel;
using Application.Aggregates.Products.ProductPriceLists.ViewModels;
using Application.Aggregates.Products.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Persistence;
using Server.Infrastructure;

namespace Server.Pages
{
    public class IndexModel (
                             ProductsApplication productsApplication,
                             ProductImagesApplication productImagesApplication,
                             ProductPriceListsApplication productPriceListsApplication) : PageModel
    {
        public List<ProductViewModel> ProductsViewModel { get; set; } = [];
        public List<ProductImageViewModel> ProductImageViewModel { get; set; } = [];
        public List<ProductPriceListViewModel> ProductPriceListViewModel { get; set; } = [];

        public async Task OnGetAsync()
        {
            ProductsViewModel = await productsApplication.GetProducts();
            ProductImageViewModel = await productImagesApplication.GetAllProductImagesAsync();
            ProductPriceListViewModel = await productPriceListsApplication.GetAllProductPriceListAsync();
        }
    }
}
