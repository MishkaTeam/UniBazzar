using Application.Aggregates.Discounts;
using Application.Aggregates.Discounts.ViewModels;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.Discounts;

public class DeleteModel(DiscountApplication application) : BasePageModel
{
    [BindProperty]
    public DiscountViewModel ViewModel { get; set; }

    public async Task OnGetAsync(Guid id)
    {
        ViewModel =  await application.GetDiscountAsync(id);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await application.DeleteDiscountAsync(ViewModel.Id);

        return RedirectToPage("Index");
    }
}
