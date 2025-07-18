using Application.Aggregates.Customers;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Pages.Profile
{
    public class Personal_InfoModel(CustomerApplication customerApplication) : BasePageModel
    {
        [BindProperty]
		public UpdateCustomerViewModel ViewModel { get; set; }

		public async Task<IActionResult> OnGetAsync(Guid Id)
        {
            ViewModel = await customerApplication.GetCustomerAsync(Id);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid Id)
        {
			await customerApplication.UpdateAsync(ViewModel);

			return Page();
        }
    }
}
