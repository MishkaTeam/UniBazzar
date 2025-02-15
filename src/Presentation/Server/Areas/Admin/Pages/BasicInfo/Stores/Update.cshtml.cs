using Application.Aggregates.Stores;
using Application.Aggregates.Stores.ViewModels;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Server.Infrastructure.Settings;

namespace Server.Areas.Admin.Pages.BasicInfo.Stores;

public class UpdateModel
	(StoresApplication storesApplication,
	ApplicationSettings applicationSettings) : BasePageModel
{
	[BindProperty]
	public StoreViewModel UpdateViewModel { get; set; } = new();
	public List<SelectListItem> CultureList { get; set; } = [];

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

		FillSelectTag();

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

	private void FillSelectTag()
	{
		var cultureSettings = applicationSettings.CultureSettings;

		CultureList =
			cultureSettings.SupportedCulture
			.Select(cultureData => new SelectListItem
			{
				Text = cultureData.Name,
				Value = cultureData.Culture
			}).ToList();

		CultureList.FirstOrDefault
			(x => x.Value == UpdateViewModel.Culture)!.Selected = true;
	}
}