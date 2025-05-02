using Application.Aggregates.Customers.ShippingAddresses;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Areas.Admin.Pages.BasicInfo.ShippingAddress
{
	public class DeleteModel(ShippingAddressApplication shippingAddressApplication) : BasePageModel
    {
        [BindProperty]
        public UpdateShippingAddressViewModel DeleteViewModel { get; set; } = new();

        public async Task OnGet(Guid Id)
        {
            DeleteViewModel = await shippingAddressApplication.GetAddress(Id);
        }
        public async Task<IActionResult> OnPost()
        {
            await shippingAddressApplication.DeleteAsync(DeleteViewModel.Id);
            return RedirectToPage("Index", new { customerId = DeleteViewModel.CustomerId.ToString() });
        }
    }
}