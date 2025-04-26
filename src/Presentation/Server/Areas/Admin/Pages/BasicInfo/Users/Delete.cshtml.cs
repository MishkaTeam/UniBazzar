using Application.Aggregates.Customer;
using Application.Aggregates.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.Users
{
    public class DeleteModel(UserApplication userApplication) : PageModel
    {
		[BindProperty]
		public UpdateUserViewModel DeleteViewModel { get; set; } = new();

		public async Task OnGet(Guid Id)
		{
			DeleteViewModel = await userApplication.GetUserAsync(Id);
		}
		public async Task<IActionResult> OnPost()
		{
			await userApplication.DeleteAsync(DeleteViewModel.Id);
			return RedirectToPage("Index");
		}
	}
}
