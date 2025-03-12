using Application.Aggregates.Products;
using Application.Aggregates.Products.ProductImages.ViewModel;
using Application.Aggregates.Products.ProductPriceLists.ViewModels;
using Application.Aggregates.Products.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Pages
{
    public class ProductsModel(ProductsApplication productsApplication) : PageModel
    {
        public List<ProductViewModel> ProductsViewModel { get; set; } = [];
        public List<ProductImageViewModel> ProductImageViewModel { get; set; } = [];
        public List<ProductPriceListViewModel> ProductPriceListViewModel { get; set; } = [];

        public async Task OnGetAsync()
        {
            ProductsViewModel = await productsApplication.GetProducts();

            ProductImageViewModel = await productsApplication.GetAllProductImagesAsync();

            ProductPriceListViewModel = await productsApplication.GetAllProductPriceListAsync();
        }
    }
}
