using Application.Aggregates.Products;
using Application.Aggregates.Products.ProductFeatures.ViewModels;
using Application.Aggregates.Products.ProductPriceLists.ViewModels;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.Products.ProductPriceLists
{
    public class UpdateModel(ProductsApplication productsApplication) : BasePageModel
    {
        [BindProperty]
        public ProductPriceListViewModel ViewModel { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToPage("../Index");
            }

            ViewModel =
                await productsApplication.GetProductPriceListAsync(id);

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
                await productsApplication.UpdatePriceList(ViewModel);
            }

            return RedirectToPage("Index",
                new { productId = ViewModel.ProductId });
        }
    }
}
