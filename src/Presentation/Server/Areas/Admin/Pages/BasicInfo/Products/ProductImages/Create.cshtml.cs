using Application.Aggregates.Products;
using Application.Aggregates.Products.ProductImages.ViewModel;
using Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using BuildingBlocks.Persistence;
using Framework.Storage;

namespace Server.Areas.Admin.Pages.BasicInfo.Products.ProductImages;

public class CreateModel(
    ProductImagesApplication productsApplication,
    IStorage storage,
    IExecutionContextAccessor executionContextAccessor) : BasePageModel
{

    [BindProperty] public CreateProductImageViewModel ViewModel { get; set; } = new();

    [BindProperty]
    [DataType(DataType.Upload)]
    [Display
    (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Picture))]
    public IFormFile UplodImage { get; set; }

    public IActionResult OnGet(Guid productId)
    {
        if (productId == Guid.Empty)
        {
            return RedirectToPage("../Index");
        }

        ViewModel.ProductId = productId;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            await UploadImageAsync(UplodImage);
            await productsApplication.CreateProductImage(ViewModel);
        }

        return RedirectToPage("Index",
            new { productId = ViewModel.ProductId.ToString() });
    }

    public async Task UploadImageAsync(IFormFile formFile)
    {

        var fileType = formFile.ContentType;
        var size = formFile.Length;

        using var memoryStream = new MemoryStream();
        await formFile.CopyToAsync(memoryStream).ConfigureAwait(false);
        string objectKey = $"PRD_IMG_{executionContextAccessor.StoreId}_{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}";
        await storage.UploadAsync("unibazzar", objectKey, memoryStream, $"{executionContextAccessor.StoreId}/ProductImages",fileType);
       var link = storage.GetPublicUrl("unibazzar", objectKey,$"{executionContextAccessor.StoreId}/ProductImages");
        ViewModel.ImageUrl = link;
        
    }
}
