using Application.Aggregates.Units.ViewModels;
using Application.Aggregates.Units;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Application.Aggregates.Customer;
using Domain.Aggregates.Customers;

namespace Server.Areas.Admin.Pages.BasicInfo.Customers
{
	public class UpdateCustomer(CustomerApplication customerApplication) : PageModel
	{
		[BindProperty]
		public UpdateCustomerViewModel UpdateViewModel { get; set; } = new();
		[BindProperty]
		public List<ShippingAddress> ShippingAddresses { get; set; } = new();
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