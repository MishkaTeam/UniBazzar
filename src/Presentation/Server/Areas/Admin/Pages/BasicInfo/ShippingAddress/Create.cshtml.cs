using Application.Aggregates.ShippingAddress;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.ShippingAddress
{
    public class CreateModel(ShippingAddressApplication shippingAddressApplication) : PageModel
    {
        [BindProperty]
        public CreateShippingAddressViewModel AddressViewModel { get; set; } = new();
        public async Task OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await shippingAddressApplication.CreateAsync(AddressViewModel);
            }
            return RedirectToPage("Index");
        }
    }
}
