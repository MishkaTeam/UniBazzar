using Application.Aggregates.Customers;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Server.Pages.Profile
{
    public class Personal_InfoModel(CustomerApplication customerApplication) : BasePageModel
    {
        [BindProperty]
		public UpdateCustomerViewModel ViewModel { get; set; }

		public async Task<IActionResult> OnGetAsync()
        {
            Guid CustomerId = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);

			if (CustomerId == Guid.Empty)
			{
				AddToastError(message: Resources.Messages.Errors.IdIsNull);
				return RedirectToPage("/Index");
			}

			ViewModel = await customerApplication.GetCustomerAsync(CustomerId);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid Id)
        {
			await customerApplication.UpdateAsync(ViewModel);

			return Page();
        }
    }
}
