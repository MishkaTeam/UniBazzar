using Application.Aggregates.Discounts;
using Application.Aggregates.Discounts.ViewModels;
using Infrastructure;
using Infrastructure.TagHelpers;
using Microsoft.AspNetCore.Mvc;

namespace Server.Areas.Admin.Pages.BasicInfo.Discounts;

public class CreateModel(DiscountApplication application) : BasePageModel
{
    [BindProperty]
    public CreateDiscountViewModel ViewModel { get; set; } = new();

    [BindProperty]
    public bool DiscountType { get; set; }

    public void OnGet()
    {
        ViewModel.Start = DateTime.UtcNow;
        ViewModel.End = DateTime.UtcNow;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (DiscountType)
        {
            ViewModel.Type = Resources.DataDictionary.DiscountPercentage;
        }
        else
        {
            ViewModel.Type = Resources.DataDictionary.DiscountFixed;
        }

        if (ModelState.IsValid)
        {
            ViewModel.Start = ViewModel.Start.ToUniversalTime();
            ViewModel.End = ViewModel.End.ToUniversalTime();
            await application.CreateDiscount(ViewModel);
        }

        return RedirectToPage("Index");
    }
}
