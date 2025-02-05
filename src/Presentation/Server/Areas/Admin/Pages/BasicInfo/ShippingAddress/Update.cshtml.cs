using Application.Aggregates.ShippingAddress;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.ShippingAddress
{
    public class UpdateModel(ShippingAddressApplication shippingAddressApplication) : PageModel
    {
        [BindProperty]
        public UpdateShippingAddressViewModel UpdateViewModel { get; set; }
        
        public async Task OnGet(Guid Id)
        {
            UpdateViewModel = await shippingAddressApplication.GetAddress(Id);
        }

        public async Task<IActionResult> OnPost()
        {
            await shippingAddressApplication.UpdateAsync(UpdateViewModel);
			return RedirectToPage("Index", new { customerId = UpdateViewModel.CustomerId.ToString() });
		}
    }
}
