using Application.Aggregates.Users;
using Domain.Aggregates.Users.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Server.Areas.Admin.Pages.BasicInfo.Users
{
    public class CreateModel(UserApplication userApplication) : PageModel
    {
        [BindProperty]
		public CreateUserViewModel CreateViewModel { get; set; }=new();
		public List<SelectListItem> Roles { get; set; } = [];
        public async Task OnGet()
		{
			Roles = [
				new SelectListItem(nameof(RoleType.Administrator), RoleType.Administrator.ToString()),
                new SelectListItem(nameof(RoleType.PosCounter), RoleType.PosCounter.ToString()),
                new SelectListItem(nameof(RoleType.Programmer), RoleType.Programmer.ToString()),
                ];

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
