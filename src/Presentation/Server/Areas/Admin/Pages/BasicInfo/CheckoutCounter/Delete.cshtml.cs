using Application.Aggregates.CheckoutCounter;
using Application.Aggregates.CheckoutCounter.ViewModels;
using Application.Aggregates.Users;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Server.Areas.Admin.Pages.BasicInfo.CheckoutCounter
{
    public class DeleteModel(CheckoutCounterApplication CheckoutCounterApplication) : PageModel
    {
        [BindProperty]
        public CheckoutCounterViewModels DeleteViewModel { get; set; } = new();
        public List<SelectListItem> CheckOutList { get; set; } = [];
        public async Task OnGet(Guid Id)
        {
            DeleteViewModel = await CheckoutCounterApplication.GetCheckoutCounterViewModels(Id);
        }
        public async Task<IActionResult> OnPost()
        {
            await CheckoutCounterApplication.DeleteAsync(DeleteViewModel.Id);
            return RedirectToPage("index");
        }
    }
}
