using Application.Aggregates.Products;
using Application.Aggregates.Products.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Pages.Product;

public class ProductsModel(ProductsApplication productsApplication) : PageModel
{
    public List<ProductCardViewModel> ProductsViewModel { get; set; } = [];

    public async Task OnGetAsync(string slug, CancellationToken ct)
    {
        ProductsViewModel = await productsApplication.GetFullProductData(slug, ct);
    }
}