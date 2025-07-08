using Application.Aggregates.Products.ProductImages;
using Application.Aggregates.Products.ProductImages.ViewModel;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.Products.ProductImages;

public class DeleteModel(ProductImagesApplication productsApplication) : BasePageModel
{
    [BindProperty]
    public ProductImageViewModel ViewModel { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            return RedirectToPage("../Index");
        }

        ViewModel = await productsApplication.GetProductImageAsync(id);

        if (ViewModel == null)
        {
            return RedirectToPage("../Index");
        }

        return Page();

    }

    public async Task<IActionResult> OnPostAsync()
    {
        await productsApplication.DeleteImage(ViewModel.Id);

        return RedirectToPage("Index",
            new { productId = ViewModel.ProductId.ToString() });
    }
}
