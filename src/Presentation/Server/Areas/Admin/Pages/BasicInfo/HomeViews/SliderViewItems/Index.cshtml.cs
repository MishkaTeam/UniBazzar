using Application.Aggregates.HomeViews;
using Application.Aggregates.HomeViews.ViewModels.HomeViews;
using Application.Aggregates.HomeViews.ViewModels.SliderViewItems;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Areas.Admin.Pages.BasicInfo.HomeViews.SliderViewItems;

public class IndexModel
    (HomeViewsApplication homeViewsApplication) : BasePageModel
{
    public List<SliderViewItemViewModel> ViewModel { get; set; } = [];
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
            (await homeViewsApplication.GetSliderItems(homeViewId)).Data!;

        return Page();
    }
}
