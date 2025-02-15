using Application.Aggregates.Stores;
using Application.Aggregates.Stores.ViewModels;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Server.Infrastructure.Settings;

namespace Server.Areas.Admin.Pages.BasicInfo.Stores;

public class CreateModel
	(StoresApplication storesApplication,
	ApplicationSettings applicationSettings) : BasePageModel
{
	[BindProperty]
	public CreateStoreViewModel CreateViewModel { get; set; } = new();
	public List<SelectListItem> CultureList { get; set; } = [];

	public void OnGet()
	{
		FillSelectTag();
	}

	public async Task<IActionResult> OnPostAsync()
	{
		if (ModelState.IsValid)
		{
			await storesApplication.CreateStoreAsync(CreateViewModel);
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
			(x => x.Value == cultureSettings.DefaultCulture.Culture)!.Selected = true;
	}
}