using Application.Aggregates.PriceLists;
using Application.Aggregates.PriceLists.ViewModels.PriceList;
using Application.Aggregates.PriceLists.ViewModels.PriceListItem;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Areas.Admin.Pages.BasicInfo.PriceLists;


public class ItemsModel(PriceListsApplication application) : BasePageModel
{

    [BindProperty]
    public CreatePriceListItemViewModel CreatePriceListItem { get; set; } = new();

    public List<PriceListItemViewModel> Items { get; set; } = [];
    public async Task OnGet(Guid Id)
    {
        CreatePriceListItem.PriceListId = Id;
        Items = (await application.GetPriceListItems(Id)).Data ?? [];
    }

    public async Task<IActionResult> OnPost(Guid Id)
    {
        CreatePriceListItem.PriceListId = Id;
        var res = await application.AddPricelistItem(CreatePriceListItem);
        Items = (await application.GetPriceListItems(Id)).Data ?? [];

        if (res.IsSuccessful)
        {
            return Page();
        }
        AddToastError(res.ErrorMessage?.Message ?? Resources.Messages.Errors.InternalError);
        return Page();
    }

    public async Task<IActionResult> OnPostDelete(Guid Id, Guid ItemId)
    {
        var res = await application.RemovePricelistItem(Id,ItemId);
        Items = (await application.GetPriceListItems(Id)).Data ?? [];

        if (res.IsSuccessful)
        {
            return Page();
        }
        AddToastError(res.ErrorMessage?.Message ?? Resources.Messages.Errors.InternalError);
        return Page();
    }
}
