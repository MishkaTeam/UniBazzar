using Application.Aggregates.PriceLists;
using Application.Aggregates.PriceLists.ViewModels.PriceList;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Areas.Admin.Pages.BasicInfo.Products.ProductPriceLists;

public class IndexModel(PriceListsApplication application) : BasePageModel
{
    public List<PriceListViewModel> ViewModel { get; set; } = [];

    public async Task<IActionResult> OnGetAsync()
    {
        ViewModel = await application.GetAllPriceListAsync();
        return Page();
    }
}
