using System.ComponentModel.DataAnnotations;
using Application.Aggregates.HomeViews;
using Application.Aggregates.HomeViews.ViewModels.SliderViewItems;
using Constants;
using Framework.Picture;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Server.Infrastructure.Services;

namespace Server.Areas.Admin.Pages.BasicInfo.HomeViews.SliderViewItems;

public class UpdateModel(
    HomeViewsApplication homeViewsApplication,
    StorageService storageService) : BasePageModel
{
    [BindProperty]
    public UpdateSliderViewItemViewModel UpdateViewModel { get; set; } = new();

    [BindProperty]
    [DataType(DataType.Upload)]
    [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Picture))]
    public IFormFile? SliderImage { get; set; }

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

        UpdateViewModel =
            sliderItemResult.Data!;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        string oldImageUrl = "";

        if (SliderImage != null)
        {
            var oldSlideItem = await homeViewsApplication
                .GetSliderItem(UpdateViewModel.HomeViewId, UpdateViewModel.Id);

            if (oldSlideItem.IsSuccessful == false)
            {
                return RedirectToPage("Index",
                    new { homeViewId = UpdateViewModel.HomeViewId.ToString() });
            }

            oldImageUrl =
                oldSlideItem.Data!.ImageUrl!;

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

            UpdateViewModel.ImageUrl = uploadResult.Data;
        }

        var result =
            await homeViewsApplication.UpdateSliderItem(UpdateViewModel);

        if (result.IsSuccessful == false)
        {
            if (SliderImage != null)
            {
                await storageService.DeleteImageAsync
                    (UpdateViewModel.ImageUrl!, Storage.SliderPath);
            }

            AddPageError
                (result.ErrorMessage!.Message);

            return Page();
        }

        if (SliderImage != null)
        {
            await storageService.DeleteImageAsync
                (oldImageUrl!, Storage.SliderPath);
        }

        await storageService.DeleteImageAsync
            (oldImageUrl!, Storage.SliderPath);

        return RedirectToPage("Index",
            new { homeViewId = UpdateViewModel.HomeViewId.ToString() });
    }
}
