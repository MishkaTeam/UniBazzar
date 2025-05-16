using Application.Aggregates.CheckoutCounter;
using Application.Aggregates.CheckoutCounter.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Server.Areas.Admin.Pages.BasicInfo.CheckoutCounter
{
    public class UpdateModel(CheckoutCounterApplication checkoutCounterApplication) : PageModel
    {
        [BindProperty]
        public CheckoutCounterViewModels UpdateViewModel { get; set; } = new();
        public List<SelectListItem> BaceCheckOut { get; set; } = [];

        public async Task OnGet(Guid Id)
        {
            UpdateViewModel = await checkoutCounterApplication.GetCheckoutCounterViewModels(Id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await checkoutCounterApplication.UpdateAsync(UpdateViewModel);
            }
            return RedirectToPage("Index");
        }

    }
}
