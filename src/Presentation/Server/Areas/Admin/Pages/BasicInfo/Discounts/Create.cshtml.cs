using Application.Aggregates.Discounts;
using Application.Aggregates.Discounts.ViewModels;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Areas.Admin.Pages.BasicInfo.Discounts;

public class CreateModel(DiscountApplication application) : BasePageModel
{
    [BindProperty]
    public CreateDiscountViewModel ViewModel { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            await application.CreateDiscount(ViewModel);
        }

        return RedirectToPage("Index");
    }
}
