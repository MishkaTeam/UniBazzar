using Application.Aggregates.PriceLists;
using Application.Aggregates.PriceLists.ViewModels.PriceList;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Areas.Admin.Pages.BasicInfo.Products.ProductPriceLists
{
    public class UpdateModel(PriceListsApplication application) : BasePageModel
    {
        [BindProperty]
        public PriceListViewModel ViewModel { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToPage("../Index");
            }

            //ViewModel =
            //    await application.GetProductPriceListAsync(id);

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
                //await application.UpdatePriceList(ViewModel);
            }

            return RedirectToPage("Index",
                new { productId = ViewModel.Id });
        }
    }
}
