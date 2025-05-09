using Application.Aggregates.Products;
using Application.Aggregates.Products.ProductImages.ViewModel;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Server.Areas.Admin.Pages.BasicInfo.Products.ProductImages;

public class UpdateModel(ProductImagesApplication productsApplication,
                         IWebHostEnvironment webHostEnvironment) : BasePageModel
{

    [BindProperty]
    public ProductImageViewModel ViewModel { get; set; } = new();

    [BindProperty]
    [DataType(DataType.Upload)]
    [Display
    (ResourceType = typeof(Resources.DataDictionary),
    Name = nameof(Resources.DataDictionary.Picture))]
    public IFormFile UplodImage { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            return RedirectToPage("../Index");
        }

        ViewModel =
            await productsApplication.GetProductImageAsync(id);

        if (ViewModel == null)
        {
            return RedirectToPage("../Index");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            UploadImage(UplodImage);
            await productsApplication.UpdateProductImage(ViewModel);
        }

        return RedirectToPage("Index",
            new { productId = ViewModel.ProductId });
    }

    public void UploadImage(IFormFile file)
    {
        var directoryPath = $"{Getwebroot()}\\ProductImage";
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        string filepath = Path.Combine(directoryPath, ViewModel.ImageUrl);
        using var output = System.IO.File.Create(filepath);
        file.CopyTo(output);
    }

    public string Getwebroot()
    {
        return webHostEnvironment.WebRootPath;
    }
}
