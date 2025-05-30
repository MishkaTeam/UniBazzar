using Application.Aggregates.Discounts;
using Application.Aggregates.Discounts.ViewModels;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Areas.Admin.Pages.BasicInfo.Discounts;

public class UpdateModel(DiscountApplication application) : BasePageModel
{
	[BindProperty]
	public DiscountViewModel ViewModel { get; set; } = new();

    [BindProperty]
    public bool DiscountType { get; set; }

    public async Task OnGetAsync(Guid id)
	{
		ViewModel = await application.GetDiscountAsync(id);
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
            await application.UpdateDiscountAsync(ViewModel);
		}
		return RedirectToPage("Index");
	}
}
