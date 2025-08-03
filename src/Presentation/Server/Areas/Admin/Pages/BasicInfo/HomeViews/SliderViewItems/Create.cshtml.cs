using System.ComponentModel.DataAnnotations;
using Application.Aggregates.HomeViews;
using Application.Aggregates.HomeViews.ViewModels.SliderViewItems;
using BuildingBlocks.Domain.Context;
using Framework.Storage;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Server.Infrastructure;

namespace Server.Areas.Admin.Pages.BasicInfo.HomeViews.SliderViewItems;

public class CreateModel(
    HomeViewsApplication homeViewsApplication,
    IExecutionContextAccessor executionContextAccessor,
    IStorage storage) : BasePageModel
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

        var imageUrl =
            await UploadImageAsync(SliderImage);

        CreateViewModel.ImageUrl = imageUrl;

        var result =
            await homeViewsApplication.AddSliderItem(CreateViewModel);

        if (result.IsSuccessful == false)
        {
            AddPageError
                (result.ErrorMessage!.Message);

            return Page();
        }

        return RedirectToPage("Index",
            new { homeViewId = CreateViewModel.HomeViewId.ToString() });
    }


    private async Task<string> UploadImageAsync(IFormFile formFile)
    {
        var size =
            formFile.Length;

        // Check Image Length

        using var memoryStream = new MemoryStream();

        await formFile.CopyToAsync
            (memoryStream).ConfigureAwait(false);

        string objectKey =
            $"SLD_IMG_{executionContextAccessor.StoreId}_{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}";

        var folder =
            $"{executionContextAccessor.StoreId}/SliderImages";

        var fileType =
            formFile.ContentType;

        await storage.UploadAsync
            ("unibazzar", objectKey, memoryStream, folder, fileType);

        var link = storage.GetPublicUrl
            ("unibazzar", objectKey, folder);

        return link;
    }
}
