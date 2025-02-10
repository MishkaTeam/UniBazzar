using Application.Aggregates.Products;
using Application.Aggregates.Products.ProductPriceLists.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.Products.ProductPriceLists;

public class CreateModel(ProductsApplication application) : PageModel
{

    [BindProperty]
    public CreateProductPriceListViewModel ViewModel { get; set; } = new();

    public IActionResult OnGet(Guid id)
    {
        if (id == Guid.Empty)
        {
            return RedirectToPage("../Index");
        }

        ViewModel.ProductId = id;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            await application.CreateProductPriceListAsync(ViewModel);
        }

        return RedirectToPage("Index",
            new { id = ViewModel.ProductId.ToString() });
    }
}
