using Application.Aggregates.Customer;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;


namespace Server.Areas.Admin.Pages.BasicInfo.Customers
{
	public class UpdateModel(CustomerApplication customerApplication) : BasePageModel
    {
		[BindProperty]
		public UpdateCustomerViewModel UpdateViewModel { get; set; } = new();
		public async Task OnGet(Guid Id)
		{
			UpdateViewModel = await customerApplication.GetCustomerAsync(Id);
		}
		public async Task<IActionResult> OnPost()
		{
			await customerApplication.UpdateAsync(UpdateViewModel);
			return RedirectToPage("Index");
		}
	}
}