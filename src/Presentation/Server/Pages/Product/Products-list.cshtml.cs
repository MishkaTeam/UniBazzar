using Application.Aggregates.PriceLists;
using Application.Aggregates.PriceLists.ViewModels.PriceList;
using Application.Aggregates.Products;
using Application.Aggregates.Products.ProductImages;
using Application.Aggregates.Products.ProductImages.ViewModel;
using Application.Aggregates.Products.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Pages
{
    public class Products_listModel(ProductsApplication productsApplication,
                           ProductImagesApplication productImagesApplication,
                           PriceListsApplication productPriceListsApplication) : PageModel
    {

        public List<ProductViewModel> ProductsViewModel { get; set; } = [];
        public List<ProductImageViewModel> ProductImageViewModel { get; set; } = [];
        public List<PriceListViewModel> ProductPriceListViewModel { get; set; } = [];

        public async Task OnGetAsync()
        {
            ProductsViewModel = await productsApplication.GetProducts();

            ProductImageViewModel = await productImagesApplication.GetAllProductImagesAsync();

            //ProductPriceListViewModel = await productPriceListsApplication.GetAllProductPriceListAsync();
        }
    }
}
