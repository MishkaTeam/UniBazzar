using System.ComponentModel.DataAnnotations;
using Application.Aggregates.HomeViews;
using Application.Aggregates.HomeViews.ViewModels.SliderViewItems;
using Constants;
using Framework.Picture;
using Framework.Storage;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Server.Infrastructure.Services;

namespace Server.Areas.Admin.Pages.BasicInfo.HomeViews.SliderViewItems;

public class CreateModel(
    HomeViewsApplication homeViewsApplication,
    StorageService storageService) : BasePageModel
{
    [BindProperty]
    public CreateSliderViewItemViewModel CreateViewModel { get; set; } = new();

    [BindProperty]
    [DataType(DataType.Upload)]
    [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Picture))]
    public IFormFile SliderImage { get; set; }

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

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        using var memoryStream =
            new MemoryStream();

        await SliderImage.CopyToAsync(memoryStream);

        var checkSize = await ImageHelper
            .CheckImageSizeAsync(memoryStream, ratio: 4);

        if (checkSize == false)
        {
            var message =
                "image size should in ratio 4.";

            AddPageError(message);
            return Page();
        }

        var uploadResult = await storageService.UploadImageAsync
            (SliderImage, Storage.SliderPrefix, Storage.SliderPath);

        if (uploadResult.IsSuccessful == false)
        {
            AddPageError
                (uploadResult.ErrorMessage!.Message);

            return Page();
        }

        CreateViewModel.ImageUrl = uploadResult.Data;

        var result =
            await homeViewsApplication.AddSliderItem(CreateViewModel);

        if (result.IsSuccessful == false)
        {
            // Delete image from bucket

            AddPageError
                (result.ErrorMessage!.Message);

            return Page();
        }

        return RedirectToPage("Index",
            new { homeViewId = CreateViewModel.HomeViewId.ToString() });
    }
}
