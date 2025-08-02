using Application.Aggregates.HomeViews;
using Application.Aggregates.HomeViews.ViewModels.SliderViewItems;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Areas.Admin.Pages.BasicInfo.HomeViews.SliderViewItems;

public class DeleteModel
    (HomeViewsApplication homeViewsApplication) : BasePageModel
{
    [BindProperty]
    public SliderViewItemViewModel DeleteViewModel { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(Guid homeViewId, Guid sliderItemId)
    {
        if (homeViewId == Guid.Empty)
        {
            return RedirectToPage("../Index");
        }

        if (sliderItemId == Guid.Empty)
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

        var sliderItemResult =
            await homeViewsApplication.GetSliderItem(homeViewId, sliderItemId);

        if (sliderItemResult.IsSuccessful == false)
        {
            return RedirectToPage("Index",
                new { homeViewId = homeViewId.ToString() });
        }

        DeleteViewModel =
            sliderItemResult.Data!;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        // Delete image from bucket

        await homeViewsApplication.DeleteSliderItem
            (DeleteViewModel.HomeViewId, DeleteViewModel.Id);

        return RedirectToPage("Index",
            new { homeViewId = DeleteViewModel.HomeViewId.ToString() });
    }
}
