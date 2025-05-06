using Application.Aggregates.Products;
using Application.Aggregates.Products.ProductImages.ViewModel;
using Application.Aggregates.Products.ProductPriceLists.ViewModels;
using Application.Aggregates.Products.ViewModels;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.Products.ProductPriceLists;

public class IndexModel(ProductsApplication productsApplication, ProductPriceListsApplication productPriceListsApplication) : BasePageModel
{
    public ProductViewModel ProductViewModel { get; set; } = new();

    public List<ProductPriceListViewModel> ViewModel { get; set; } = [];

    public async Task<IActionResult> OnGetAsync(Guid productId)
    {
        if (productId == Guid.Empty)
        {
            return RedirectToPage("../Index");
        }

        ProductViewModel = await productsApplication.GetProductAsync(productId);

        ViewModel = await productPriceListsApplication.GetPriceListByProductId(productId);

        return Page();
    }
}
