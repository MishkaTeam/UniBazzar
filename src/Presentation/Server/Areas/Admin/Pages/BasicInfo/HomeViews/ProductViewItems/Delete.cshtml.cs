using Application.Aggregates.HomeViews;
using Application.Aggregates.HomeViews.ViewModels.ProductViewItems;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Areas.Admin.Pages.BasicInfo.HomeViews.ProductViewItems;

public class DeleteModel
    (HomeViewsApplication homeViewsApplication) : BasePageModel
{
    [BindProperty]
    public ProductViewItemViewModel DeleteViewModel { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(Guid homeViewId, Guid productItemId)
    {
        if (homeViewId == Guid.Empty)
        {
            return RedirectToPage("../Index");
        }

        if (productItemId == Guid.Empty)
        {
            return RedirectToPage("Index",
                new { homeViewId = homeViewId.ToString() });
        }

        var homeViewResult =
            await homeViewsApplication.GetHomeViewAsync(homeViewId);

        if (homeViewResult.IsSuccessful == false)
        {
            return RedirectToPage("../Index");
        }

        var productItemResult =
            await homeViewsApplication.GetProductItem(homeViewId, productItemId);

        if (productItemResult.IsSuccessful == false)
        {
            return RedirectToPage("Index",
                new { homeViewId = homeViewId.ToString() });
        }

        DeleteViewModel =
            productItemResult.Data!;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await homeViewsApplication.DeleteProductItem
            (DeleteViewModel.HomeViewId, DeleteViewModel.Id);

        return RedirectToPage("Index",
            new { homeViewId = DeleteViewModel.HomeViewId.ToString() });
    }
}
