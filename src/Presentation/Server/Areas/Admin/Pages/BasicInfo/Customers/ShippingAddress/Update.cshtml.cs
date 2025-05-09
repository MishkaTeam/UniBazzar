using Application.Aggregates.Customers.ShippingAddresses;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Areas.Admin.Pages.BasicInfo.ShippingAddress
{
	public class UpdateModel(ShippingAddressApplication shippingAddressApplication) : BasePageModel
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
