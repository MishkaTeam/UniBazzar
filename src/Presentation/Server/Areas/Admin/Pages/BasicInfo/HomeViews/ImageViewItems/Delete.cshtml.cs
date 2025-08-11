using Application.Aggregates.HomeViews;
using Application.Aggregates.HomeViews.ViewModels.ImageViewItems;
using Constants;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Resources;
using Server.Infrastructure.Services;

namespace Server.Areas.Admin.Pages.BasicInfo.HomeViews.ImageViewItems;

public class DeleteModel(
    HomeViewsApplication homeViewsApplication,
    StorageService storageService) : BasePageModel
{
    [BindProperty]
    public ImageViewItemViewModel DeleteViewModel { get; set; } = new();
    public List<SelectListItem> ColumnTypes { get; set; } = new();

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

        FillSelectTag();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await storageService.DeleteImageAsync
            (DeleteViewModel.ImageUrl!, Storage.ImagePath);

        await homeViewsApplication.DeleteImageItem
            (DeleteViewModel.HomeViewId, DeleteViewModel.Id);

        return RedirectToPage("Index",
            new { homeViewId = DeleteViewModel.HomeViewId.ToString() });
    }


    private void FillSelectTag()
    {
        var columnTypes =
            new Dictionary<string, string>()
            {
                {"1", DataDictionary.FullScreen},
                {"2", DataDictionary.SplitScreen},
            };

        foreach (var columnType in columnTypes)
        {
            ColumnTypes.Add(new SelectListItem()
            {
                Text = columnType.Value,
                Value = columnType.Key,
            });
        }
    }
}
