using System.ComponentModel.DataAnnotations;
using Application.Aggregates.HomeViews;
using Application.Aggregates.HomeViews.ViewModels.ImageViewItems;
using Constants;
using Framework.Picture;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Resources;
using Server.Infrastructure.Services;

namespace Server.Areas.Admin.Pages.BasicInfo.HomeViews.ImageViewItems;

public class UpdateModel(
    HomeViewsApplication homeViewsApplication,
    StorageService storageService) : BasePageModel
{
    [BindProperty]
    public UpdateImageViewItemViewModel UpdateViewModel { get; set; } = new();
    public List<SelectListItem> ColumnTypes { get; set; } = new();


    [BindProperty]
    [DataType(DataType.Upload)]
    [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Picture))]
    public IFormFile? Image { get; set; }

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

        UpdateViewModel =
            imageItemResult.Data!;

        FillSelectTag();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            FillSelectTag();

            return Page();
        }

        if (Image != null)
        {
            // Delete old image from bucket

            using var memoryStream =
                new MemoryStream();

            await Image.CopyToAsync(memoryStream);

            var checkSize = await ImageHelper
                .CheckImageSizeAsync(memoryStream, 300, 200);

            if (checkSize == false)
            {
                var message =
                    "image size is incorrect.";

                AddPageError(message);

                FillSelectTag();
                return Page();

            }

            var uploadResult = await storageService.UploadImageAsync
                (Image, Storage.ImagePrefix, Storage.ImagePath);

            if (uploadResult.IsSuccessful == false)
            {
                AddPageError
                    (uploadResult.ErrorMessage!.Message);

                FillSelectTag();

                return Page();
            }

            UpdateViewModel.ImageUrl = uploadResult.Data;
        }

        var result =
            await homeViewsApplication.UpdateImageItem(UpdateViewModel);

        if (result.IsSuccessful == false)
        {
            if (Image != null)
            {
                // Delete new image from bucket
            }

            AddPageError
                (result.ErrorMessage!.Message);

            FillSelectTag();

            return Page();
        }

        return RedirectToPage("Index",
            new { homeViewId = UpdateViewModel.HomeViewId.ToString() });
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
