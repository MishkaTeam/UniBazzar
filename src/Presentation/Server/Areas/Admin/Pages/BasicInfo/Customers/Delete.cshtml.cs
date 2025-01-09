using Application.Aggregates.Customer;
using Application.Aggregates.Units;
using Domain.Aggregates.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.Customers
{
    public class DeleteModel(CustomerApplication customerApplication) : PageModel
    {
		[BindProperty]
		public CreateCustomerViewModel DeleteViewModel { get; set; } = new();
		[BindProperty]
		public List<ShippingAddress> ShippingAddresses { get; set; } = new();
		public async Task OnGet(Guid Id)
		{
			DeleteViewModel = await customerApplication.GetCustomerAsync(Id);
		}
		public async Task<IActionResult> OnPost()
		{
			if (ModelState.IsValid)
			{
				await customerApplication.DeleteAsync(DeleteViewModel.Id);
			}
				return RedirectToPage("Index");
			
		}
	}
}
