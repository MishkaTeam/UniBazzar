using Application.Aggregates.ShippingAddress;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.ShippingAddress
{
    public class DeleteModel(ShippingAddressApplication shippingAddressApplication) : PageModel
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