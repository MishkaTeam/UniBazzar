using Application.Aggregates.Customers.ShippingAddresses;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Pages.Profile
{
    public class AddressesModel(ShippingAddressApplication shippingAddress) : BasePageModel
    {
        [BindProperty]
        public CreateShippingAddressViewModel CreateAddress { get; set; } = new();
        [BindProperty]
		public Guid CustomerId { get; set; }

        public List<UpdateShippingAddressViewModel> ViewModel { get; set; } = new();

		public async Task<IActionResult> OnGetAsync(Guid Id)
        {
            CustomerId = Id;
            ViewModel = await shippingAddress.GetAllAddress(Id);

            return Page();
        }

        public async Task<IActionResult> OnPostCreate()
        {
            CreateAddress.CustomerId = CustomerId;
            await shippingAddress.CreateAsync(CreateAddress);
            CreateAddress = new();

			ViewModel = await shippingAddress.GetAllAddress(CustomerId);

			return Page();
        }

        public async Task<IActionResult> OnPostDelete(Guid Id)
        { 
            await shippingAddress.DeleteAsync(Id);

			ViewModel = await shippingAddress.GetAllAddress(CustomerId);

			return Page();
        }
    }
}
