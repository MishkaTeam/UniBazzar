using Application.Aggregates.Customer;
using Domain.Aggregates.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Server.Areas.Admin.Pages.BasicInfo.Customers
{
	public class CreateModel(CustomerApplication customerApplication) : PageModel
	{
		[BindProperty]
		public CreateCustomerViewModel CreateViewModel { get; set; }=new();
		[BindProperty]
		public List<ShippingAddress> ShippingAddresses { get; set; } = new();
		public async Task OnGet()
		{
			
		}
		public async Task<IActionResult> OnPost()
		{
			if (ModelState.IsValid)
			{
				await customerApplication.CreateAsync(CreateViewModel);
			}
			return RedirectToPage("Index");
		}
	}

}
