using Application.Aggregates.Discounts;
using Application.Aggregates.Discounts.ViewModels;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Areas.Admin.Pages.BasicInfo.Discounts;

public class UpdateModel(DiscountApplication application) : BasePageModel
{
	[BindProperty]
	public DiscountViewModel ViewModel { get; set; } = new();

	public async Task OnGetAsync(Guid id)
	{
		ViewModel = await application.GetDiscountAsync(id);
	}

	public async Task<IActionResult> OnPostAsync()
	{
		if (ModelState.IsValid)
		{
			await application.UpdateDiscountAsync(ViewModel);
		}
		return RedirectToPage("Index");
	}
}
