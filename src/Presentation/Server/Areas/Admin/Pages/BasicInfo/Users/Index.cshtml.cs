using Application.Aggregates.Customer;
using Application.Aggregates.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.Users
{
    public class IndexModel(UserApplication userApplication) : PageModel
    {

        public List<UpdateUserViewModel> ViewModel { get; set; } = [];
        public async Task OnGet()
        {
            ViewModel = await userApplication.GetAllUser();
        }
    }
}
