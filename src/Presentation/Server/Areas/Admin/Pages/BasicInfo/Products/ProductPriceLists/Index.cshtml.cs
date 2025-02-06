using Application.Aggregates.Products;
using Application.Aggregates.Products.ProductPriceLists.ViewModels;
using Application.Aggregates.Products.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.Products.ProductPriceLists;

public class IndexModel(ProductsApplication application) : PageModel
{

    public ProductViewModel ProductViewModel { get; set; }

    public List<ProductPriceListViewModel> ViewModel { get; set; } = [];

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            return RedirectToPage("../Index");
        }

        ProductViewModel = await application.GetProductAsync(id);

        ViewModel = await application.GetPriceListByProductId(id);

        return Page();
    }
}
