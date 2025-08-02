using Application.Aggregates.HomeViews;
using Application.Aggregates.HomeViews.ViewModels.ProductViewItems;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Areas.Admin.Pages.BasicInfo.HomeViews.ProductViewItems;

public class IndexModel
    (HomeViewsApplication homeViewsApplication) : BasePageModel
{
    public List<ProductViewItemViewModel> ViewModel { get; set; } = [];
    public Guid HomeViewId { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid homeViewId)
    {
        if (homeViewId == Guid.Empty)
        {
            return RedirectToPage("../Index");
        }

        HomeViewId = homeViewId;

        ViewModel =
            (await homeViewsApplication.GetProductItems(homeViewId)).Data!;

        return Page();
    }
}
