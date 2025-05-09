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
        public List<SelectListItem> BaseCheckoutCounterList { get; set; } = [];
        public async void OnGet()
        {
            await FillCheckoutCountersBaseCheckoutCounters();
        }
        public async Task<IActionResult> OnPost()
        {
            await FillCheckoutCountersBaseCheckoutCounters();
            if (ModelState.IsValid)
            {
                await checkoutCounterApplication.CreateAsync(CreateViewModel);
            }
            return RedirectToPage("Index");
        }

        private async Task FillCheckoutCountersBaseCheckoutCounters()
        {
            var baseListCheckoutCounters = await checkoutCounterApplication.GetCheckoutCounters();

            BaseCheckoutCounterList = baseListCheckoutCounters.Select(CheckoutCounter => new SelectListItem
            {
                Disabled = false,
                Text = CheckoutCounter.Name,
                Value = CheckoutCounter.Id.ToString()
            }).ToList();
            
            BaseCheckoutCounterList.Add(new SelectListItem
            {
                Selected = true,
                Text = DataDictionary.Name,
                Value = Guid.Empty.ToString()
            });
        }
    }
}
