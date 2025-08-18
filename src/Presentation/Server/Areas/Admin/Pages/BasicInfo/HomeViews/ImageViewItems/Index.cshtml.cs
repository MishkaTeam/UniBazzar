using Application.Aggregates.HomeViews;
using Application.Aggregates.HomeViews.ViewModels.HomeViews;
using Application.Aggregates.HomeViews.ViewModels.ImageViewItems;
using Domain.Aggregates.Cms.HomeViews;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Areas.Admin.Pages.BasicInfo.HomeViews.ImageViewItems;

public class IndexModel
    (HomeViewsApplication homeViewsApplication) : BasePageModel
{
    public List<ImageViewItemViewModel> ViewModel { get; set; } = [];
    public HomeViewViewModel HomeView { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(Guid homeViewId)
    {
        if (homeViewId == Guid.Empty)
        {
            return RedirectToPage("../Index");
        }

        var result =
            await homeViewsApplication.GetHomeViewAsync(homeViewId);

        if (result.IsSuccessful == false)
        {
            return RedirectToPage("../Index");
        }

        HomeView = result.Data!;

        ViewModel =
            (await homeViewsApplication.GetImageItems(homeViewId)).Data!;

        return Page();
    }
}
