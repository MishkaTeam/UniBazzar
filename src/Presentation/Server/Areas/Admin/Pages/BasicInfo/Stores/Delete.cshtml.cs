using Application.Aggregates.Stores;
using Application.Aggregates.Stores.ViewModels;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Areas.Admin.Pages.BasicInfo.Stores;

public class DeleteModel
	(StoresApplication storesApplication) : BasePageModel
{
	[BindProperty]
	public StoreViewModel DeleteViewModel { get; set; } = new();

	public async Task<IActionResult> OnGetAsync(Guid id)
	{
		if (id == Guid.Empty)
		{
			return RedirectToPage("Index");
		}

		DeleteViewModel =
			await storesApplication.GetStoreAsync(id);

		if (DeleteViewModel == null)
		{
			return RedirectToPage("Index");
		}

		return Page();
	}

	public async Task<IActionResult> OnPostAsync()
	{
		await storesApplication.DeleteStoreAsync(DeleteViewModel.Id);

		return RedirectToPage("Index");
	}
}