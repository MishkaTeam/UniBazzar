using Application.Aggregates.Products;
using Application.Aggregates.Products.ProductImages.ViewModel;
using Application.Aggregates.Products.ProductPriceLists.ViewModels;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.Products.ProductPriceLists;

public class CreateModel(ProductsApplication productsApplication) : BasePageModel
{

    [BindProperty]
    public CreateProductPriceListViewModel ViewModel { get; set; } = new();

    public IActionResult OnGet(Guid productId)
    {
        if (productId == Guid.Empty)
        {
            return RedirectToPage("../Index");
        }

        ViewModel.ProductId = productId;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            await productsApplication.CreateProductPriceList(ViewModel);
        }

        return RedirectToPage("Index",
            new { productId = ViewModel.ProductId.ToString() });
    }
}
