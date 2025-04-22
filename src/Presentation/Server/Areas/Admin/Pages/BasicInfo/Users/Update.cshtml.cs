using Application.Aggregates.Customer;
using Application.Aggregates.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.Users
{
    public class UpdateModel(UserApplication userApplication) : PageModel
    {
		[BindProperty]
		public UpdateUserViewModel UpdateViewModel { get; set; } = new();
		public async Task OnGet(Guid Id)
		{
			UpdateViewModel = await userApplication.GetUserAsync(Id);
		}
		public async Task<IActionResult> OnPost()
		{
			await userApplication.UpdateAsync(UpdateViewModel);
			return RedirectToPage("Index");
		}
	}
}
