using Application.Aggregates.HomeViews;
using Application.Aggregates.HomeViews.ViewModels.SliderViewItems;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Areas.Admin.Pages.BasicInfo.HomeViews.SliderViewItems;

public class IndexModel
    (HomeViewsApplication homeViewsApplication) : BasePageModel
{
    public List<SliderViewItemViewModel> ViewModel { get; set; } = [];
    public Guid HomeViewId { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid homeViewId)
    {
        if (homeViewId == Guid.Empty)
        {
            return RedirectToPage("../Index");
        }

        HomeViewId = homeViewId;

        ViewModel =
            (await homeViewsApplication.GetSliderItems(homeViewId)).Data!;

        return Page();
    }
}
