using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Aggregates.Customer;


namespace Server.Areas.Admin.Pages.BasicInfo.Customers
{
	public class UpdateModel(CustomerApplication customerApplication) : PageModel
	{
		[BindProperty]
		public UpdateCustomerViewModel UpdateViewModel { get; set; } = new();
		public async Task OnGet(Guid Id)
		{
			UpdateViewModel = await customerApplication.GetRootCustomersAsync(Id);
		}
		public async Task<IActionResult> OnPost()
		{
			await customerApplication.UpdateAsync(UpdateViewModel);
			return RedirectToAction("Index");
		}
	}
}