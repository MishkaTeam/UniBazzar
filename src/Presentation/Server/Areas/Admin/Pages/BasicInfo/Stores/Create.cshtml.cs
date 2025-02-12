using Application.Aggregates.Stores;
using Application.Aggregates.Stores.ViewModels;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Areas.Admin.Pages.BasicInfo.Stores;

public class CreateModel
	(StoresApplication storesApplication) : BasePageModel
{
	[BindProperty]
	public CreateStoreViewModel CreateViewModel { get; set; } = new();

	public void OnGet()
	{
	}

	public async Task<IActionResult> OnPostAsync()
	{
		if (ModelState.IsValid)
		{
			await storesApplication.CreateStoreAsync(CreateViewModel);
		}

		return RedirectToPage("Index");
	}
}