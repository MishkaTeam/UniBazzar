using Application.Aggregates.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.Users
{
    public class CreateModel(UserApplication userApplication) : PageModel
    {
        [BindProperty]
		public CreateUserViewModel CreateViewModel { get; set; }=new();
		public async Task OnGet()
		{
			
		}
		public async Task<IActionResult> OnPost()
		{
			if (ModelState.IsValid)
			{
				await userApplication.CreateAsync(CreateViewModel);
			}
			return RedirectToPage("Index");
		}
    }
}
