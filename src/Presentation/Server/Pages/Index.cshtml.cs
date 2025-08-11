using Application.Aggregates.HomeViews;
using Application.Aggregates.HomeViews.ViewModels.HomeViews;
using Application.Aggregates.PriceLists;
using Application.Aggregates.Products;
using Application.Aggregates.Products.ProductImages;
using Application.Aggregates.Products.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Pages;

public class IndexModel (
    ProductsApplication productsApplication,
    HomeViewsApplication homeViewsApplication,
    ProductImagesApplication productImagesApplication,
    PriceListsApplication productPriceListsApplication) : PageModel
{
    public List<ProductCardViewModel> ProductsViewModel { get; set; } = [];
    public List<IndexHomeViewViewModel> HomeViews { get; set; } = [];

    public async Task OnGetAsync()
    {
        ProductsViewModel =
            await productsApplication.GetIndexProducts();

        HomeViews = 
            (await homeViewsApplication.GetIndexHomeViews()).Data!;
    }
}
