using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Aggregates.Customer;


namespace Server.Areas.Admin.Pages.BasicInfo.Customers
{
	public class UpdateCustomer(CustomerApplication customerApplication) : PageModel
	{
		[BindProperty]
		public UpdateCustomerViewModel UpdateViewModel { get; set; } = new();
		public async Task OnGet(Guid Id)
		{
			UpdateViewModel = await customerApplication.GetCustomerAsync(Id);
		}
		public async Task<IActionResult> OnPost()
		{
			if (ModelState.IsValid)
			{
				await customerApplication.UpdateAsync(UpdateViewModel);
			}
			return RedirectToAction("Index");
		}
	}
}