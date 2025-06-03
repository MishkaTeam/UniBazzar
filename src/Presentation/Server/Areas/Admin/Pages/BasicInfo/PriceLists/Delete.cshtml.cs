using Application.Aggregates.PriceLists;
using Application.Aggregates.PriceLists.ViewModels.PriceList;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Areas.Admin.Pages.BasicInfo.Products.ProductPriceLists;

public class DeleteModel(PriceListsApplication application) : BasePageModel
{
    [BindProperty]
    public PriceListViewModel ViewModel { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            return RedirectToPage("Index");
        }

        ViewModel =
           await application.GetPriceListAsync(id);

        if (ViewModel == null)
        {
            return RedirectToPage("Index");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var res = await application.DeletePriceList(ViewModel.Id);

        if (res.IsSuccessful)
        {
            return RedirectToPage("Index", new { productId = ViewModel.Id });
        }

        AddToastError(res.ErrorMessage?.Message ?? Resources.Messages.Errors.InternalError);
        return Page();
    }
}
