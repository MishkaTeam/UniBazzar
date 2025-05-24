using Application.Aggregates.PriceLists;
using Application.Aggregates.PriceLists.ViewModels.PriceList;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Areas.Admin.Pages.BasicInfo.Products.ProductPriceLists;

public class CreateModel(PriceListsApplication productsApplication) : BasePageModel
{

    [BindProperty]
    public CreatePriceListViewModel ViewModel { get; set; } = new();

    public IActionResult OnGet(Guid productId)
    {
        if (productId == Guid.Empty)
        {
            return RedirectToPage("Index");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            await productsApplication.CreatePriceList(ViewModel);
        }

        return RedirectToPage("Index");
    }
}
