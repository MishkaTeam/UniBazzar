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

public class CreateModel(
    HomeViewsApplication homeViewsApplication,
    StorageService storageService) : BasePageModel
{
    [BindProperty]
    public CreateImageViewItemViewModel CreateViewModel { get; set; } = new();
    public List<SelectListItem> ColumnTypes { get; set; } = new();

    [BindProperty]
    [DataType(DataType.Upload)]
    [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Picture))]
    public IFormFile Image { get; set; }

    public async Task<IActionResult> OnGet(Guid homeViewId)
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

        CreateViewModel.HomeViewId = homeViewId;

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

        CreateViewModel.ImageUrl = uploadResult.Data;

        var result =
            await homeViewsApplication.AddImageItem(CreateViewModel);

        if (result.IsSuccessful == false)
        {
            // Delete image from bucket

            AddPageError
                (result.ErrorMessage!.Message);

            FillSelectTag();

            return Page();
        }

        return RedirectToPage("Index",
            new { homeViewId = CreateViewModel.HomeViewId.ToString() });
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
