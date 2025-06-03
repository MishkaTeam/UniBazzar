using Application.Aggregates.Products;
using Application.Aggregates.Products.ProductImages;
using Application.Aggregates.Products.ProductImages.ViewModel;
using Application.Aggregates.Products.ViewModels;
using Domain.Aggregates.Products;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.Products.ProductImages;

public class IndexModel(ProductsApplication productsApplication, ProductImagesApplication productImagesApplication) : BasePageModel
{
    public ProductViewModel ProductViewModel { get; set; } = new();

    public List<ProductImageViewModel> ViewModel { get; set; } = [];

    public async Task<IActionResult> OnGetAsync(Guid productId)
    {
        if (productId == Guid.Empty)
        {
            return RedirectToPage("../Index");
        }

        ProductViewModel = await productsApplication.GetProductAsync(productId);

        ViewModel = await productImagesApplication.GetImageByProductIdAsync(productId);

        return Page();
    }
}
