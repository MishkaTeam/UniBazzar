using Application.Aggregates.Customers.ShippingAddresses;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Server.Pages.Profile
{
    public class AddressesModel(ShippingAddressApplication shippingAddress) : BasePageModel
    {
        [BindProperty]
        public CreateShippingAddressViewModel CreateAddress { get; set; } = new();
        [BindProperty]
		public Guid CustomerId { get; set; }

        public List<UpdateShippingAddressViewModel> ViewModel { get; set; } = new();

		public async Task<IActionResult> OnGetAsync()
        {
            CustomerId = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);

			if (CustomerId == Guid.Empty)
			{
                AddToastError(message: Resources.Messages.Errors.IdIsNull);
                return RedirectToPage("/Index");
			}

			ViewModel = await shippingAddress.GetAllAddress(CustomerId);

            return Page();
        }

        public async Task<IActionResult> OnPostCreate()
        {
            CreateAddress.CustomerId = CustomerId;
            await shippingAddress.CreateAsync(CreateAddress);

			return RedirectToPage(new { Id = CustomerId });
        }

        public async Task<IActionResult> OnPostDelete(Guid Id)
        { 
            await shippingAddress.DeleteAsync(Id);

			ViewModel = await shippingAddress.GetAllAddress(CustomerId);

			return Page();
        }
    }
}
