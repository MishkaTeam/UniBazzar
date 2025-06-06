using Application.Aggregates.PriceLists;
using Application.Aggregates.PriceLists.ViewModels.PriceList;
using Application.Aggregates.Products;
using Application.Aggregates.Products.ProductImages.ViewModel;
using Application.Aggregates.Products.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Pages
{
    public class IndexModel (
                             ProductsApplication productsApplication,
                             ProductImagesApplication productImagesApplication,
                             PriceListsApplication productPriceListsApplication) : PageModel
    {
        public List<ProductCardViewModel> ProductsViewModel { get; set; } = [];
        public async Task OnGetAsync()
        {
            ProductsViewModel = await productsApplication.GetIndexProducts();
        }
    }
}
