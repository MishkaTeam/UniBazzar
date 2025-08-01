using Application.Aggregates.HomeViews;
using Application.Aggregates.HomeViews.ViewModels.ImageViewItems;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Areas.Admin.Pages.BasicInfo.HomeViews.ImageViewItems;

public class DeleteModel
    (HomeViewsApplication homeViewsApplication) : BasePageModel
{
    [BindProperty]
    public ImageViewItemViewModel DeleteViewModel { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(Guid homeViewId, Guid imageItemId)
    {
        if (homeViewId == Guid.Empty)
        {
            return RedirectToPage("../Index");
        }

        if (imageItemId == Guid.Empty)
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

        var imageItemResult =
            await homeViewsApplication.GetImageItem(homeViewId, imageItemId);

        if (imageItemResult.IsSuccessful == false)
        {
            return RedirectToPage("Index",
                new { homeViewId = homeViewId.ToString() });
        }

        DeleteViewModel =
            imageItemResult.Data!;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        // Delete image from bucket

        await homeViewsApplication.DeleteImageItem
            (DeleteViewModel.HomeViewId, DeleteViewModel.Id);

        return RedirectToPage("Index",
            new { homeViewId = DeleteViewModel.HomeViewId.ToString() });
    }
}
