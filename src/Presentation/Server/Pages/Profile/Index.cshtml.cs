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
        public UpdateCustomerViewModel ViewModel { get; set; } = new();

		public async Task<IActionResult> OnGetAsync()
        {
			if (User == null || User.Identity == null || User.Identity.IsAuthenticated == false)
			{
				AddToastError(message: Resources.Messages.Errors.IdIsNull);
				return RedirectToPage("/Index");
			}

            Guid CustomerId = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);

			ViewModel = await customerApplication.GetCustomerAsync(CustomerId);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
			await customerApplication.UpdateAsync(ViewModel);

			return Page();
        }
    }
}
