using Application.Aggregates.Customer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.Customers
{
	public class CreateModel(CustomerApplication customerApplication) : PageModel
	{
		[BindProperty]
		public CreateCustomerViewModel CreateViewModel { get; set; }=new();
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
