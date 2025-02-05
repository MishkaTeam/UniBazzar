using Application.Aggregates.Customer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.Customers
{
	public class DeleteModel(CustomerApplication customerApplication) : PageModel
	{
		[BindProperty]
		public UpdateCustomerViewModel DeleteViewModel { get; set; } = new();

		public async Task OnGet(Guid Id)
		{
			DeleteViewModel = await customerApplication.GetCustomerAsync(Id);
		}
		public async Task<IActionResult> OnPost()
		{
			await customerApplication.DeleteAsync(DeleteViewModel.Id);
			return RedirectToPage("Index");
		}
	}
}
