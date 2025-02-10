using Application.Aggregates.ShippingAddress;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.ShippingAddress
{
    public class CreateModel(ShippingAddressApplication shippingAddressApplication) : BasePageModel
    {
        [BindProperty]
        public CreateShippingAddressViewModel AddressViewModel { get; set; } = new();
        public IActionResult OnGet(Guid customerId)
        {
            if (customerId == Guid.Empty)
            {
                return RedirectToPage("Index", new { customerId = AddressViewModel.CustomerId.ToString() });
            }
            AddressViewModel.CustomerId = customerId;
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await shippingAddressApplication.CreateAsync(AddressViewModel);
            }
            return RedirectToPage("Index", new { customerId = AddressViewModel.CustomerId.ToString() });
        }
    }
}
