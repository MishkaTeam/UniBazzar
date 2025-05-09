using Application.Aggregates.Products;
using Application.Aggregates.Products.ProductImages.ViewModel;
using Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Server.Areas.Admin.Pages.BasicInfo.Products.ProductImages;

public class CreateModel(ProductImagesApplication productsApplication,
                         IWebHostEnvironment webHostEnvironment) : BasePageModel
{

    [BindProperty]
    public CreateProductImageViewModel ViewModel { get; set; } = new();

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
            UploadImage(UplodImage);
            await productsApplication.CreateProductImage(ViewModel);
        }

        return RedirectToPage("Index",
            new { productId = ViewModel.ProductId.ToString() });
    }

    public void UploadImage(IFormFile file)
    {
        var directoryPath = $"{Getwebroot()}\\ProductImage";
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        ViewModel.ImageUrl = Path.Combine(Guid.NewGuid().ToString() + ".png");
        string filepath = Path.Combine(directoryPath, ViewModel.ImageUrl);
        using var output = System.IO.File.Create(filepath);
        file.CopyTo(output);
    }

    public string Getwebroot()
    {
        return webHostEnvironment.WebRootPath;
    }
}
