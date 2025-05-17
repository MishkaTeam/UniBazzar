using Application.Aggregates.CheckoutCounter;
using Application.Aggregates.CheckoutCounter.ViewModels;
using Application.Aggregates.Units;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Resources;

namespace Server.Areas.Admin.Pages.BasicInfo.CheckoutCounter
{
    public class CreateModel(CheckoutCounterApplication checkoutCounterApplication) : BasePageModel
    {
        [BindProperty]
        public CreateCheckoutCounterViewModels CreateViewModel { get; set; } = new();
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await checkoutCounterApplication.CreateAsync(CreateViewModel);
            }
            return RedirectToPage("Index");
        }

    }
}
