using Application.Aggregates.HomeViews;
using Application.Aggregates.HomeViews.ViewModels.ImageViewItems;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Areas.Admin.Pages.BasicInfo.HomeViews.ImageViewItems;

public class IndexModel
(HomeViewsApplication homeViewsApplication) : BasePageModel
{
    public List<ImageViewItemViewModel> ViewModel { get; set; } = [];
    public Guid HomeViewId { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid homeViewId)
    {
        if (homeViewId == Guid.Empty)
        {
            return RedirectToPage("../Index");
        }

        HomeViewId = homeViewId;

        ViewModel =
            (await homeViewsApplication.GetImageItems(homeViewId)).Data!;

        return Page();
    }

}
