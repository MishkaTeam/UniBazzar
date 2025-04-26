using Application.Aggregates.Customer;
using Application.Aggregates.Users;
using Domain.Aggregates.Users.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Server.Areas.Admin.Pages.BasicInfo.Users
{
    public class UpdateModel(UserApplication userApplication) : PageModel
    {
        [BindProperty]
        public UpdateUserViewModel UpdateViewModel { get; set; } = new();

        public List<SelectListItem> Roles { get; set; } = [];

        public async Task OnGet(Guid Id)
        {
            Roles = [
                new SelectListItem(nameof(RoleType.Administrator), RoleType.Administrator.ToString()),
                new SelectListItem(nameof(RoleType.PosCounter), RoleType.PosCounter.ToString()),
                new SelectListItem(nameof(RoleType.Programmer), RoleType.Programmer.ToString()),
                ];

            UpdateViewModel = await userApplication.GetUserAsync(Id);
        }
        public async Task<IActionResult> OnPost()
        {
            await userApplication.UpdateAsync(UpdateViewModel);
            return RedirectToPage("Index");
        }
    }
}
