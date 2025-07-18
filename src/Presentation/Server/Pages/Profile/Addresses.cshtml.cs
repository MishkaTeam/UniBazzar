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
        public UpdateShippingAddressViewModel UpdateAddress { get; set; } = new();

		[BindProperty]
		public Guid CustomerId { get; set; }

        public List<UpdateShippingAddressViewModel> ViewModel { get; set; } = new();

		public async Task<IActionResult> OnGetAsync()
        {
			if (User == null || User.Identity == null || User.Identity.IsAuthenticated == false)
			{
				AddToastError(message: Resources.Messages.Errors.IdIsNull);
				return RedirectToPage("/Index");
			}

            CustomerId = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);

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
            UpdateAddress = new();

			return Page();
        }

        public async Task<IActionResult> OnPostUpdate(Guid Id, string check)
        {
			if (check == "Get")
			{
			    UpdateAddress = await shippingAddress.GetAddress(Id);
			}
			else if(check == "Update")
			{
                UpdateAddress.CustomerId = CustomerId;
                await shippingAddress.UpdateAsync(UpdateAddress);
                UpdateAddress = new();
			}

			ViewModel = await shippingAddress.GetAllAddress(CustomerId);

			return Page();
        }
    }
}
