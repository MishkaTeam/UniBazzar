using Application.Aggregates.Stores;
using Application.Aggregates.Stores.ViewModels;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Areas.Admin.Pages.BasicInfo.Stores;

public class UpdateModel
	(StoresApplication storesApplication) : BasePageModel
{
	[BindProperty]
	public StoreViewModel UpdateViewModel { get; set; } = new();

	public async Task<IActionResult> OnGetAsync(Guid id)
	{
		if (id == Guid.Empty)
		{
			return RedirectToPage("Index");
		}

		UpdateViewModel =
			await storesApplication.GetStoreAsync(id);

		if (UpdateViewModel == null)
		{
			return RedirectToPage("Index");
		}

		return Page();
	}

	public async Task<IActionResult> OnPostAsync()
	{
		if (ModelState.IsValid)
		{
			await storesApplication.UpdateStoreAsync(UpdateViewModel);
		}

		return RedirectToPage("Index");
	}
}