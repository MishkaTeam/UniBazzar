using Application.Aggregates.PriceLists;
using Application.Aggregates.PriceLists.ViewModels.PriceList;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Areas.Admin.Pages.BasicInfo.Products.ProductPriceLists;

public class CreateModel(PriceListsApplication productsApplication) : BasePageModel
{

    [BindProperty]
    public CreatePriceListViewModel CreateViewModel { get; set; } = new();

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            await productsApplication.CreatePriceList(CreateViewModel);
        }

        return RedirectToPage("Index");
    }
}
