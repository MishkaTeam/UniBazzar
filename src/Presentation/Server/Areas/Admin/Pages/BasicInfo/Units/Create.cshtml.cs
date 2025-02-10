using System.Security.Cryptography.Xml;
using Application.Aggregates.Units;
using Application.Aggregates.Units.ViewModels;
using Infrastructure;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Resources;

namespace Server.Areas.Admin.Pages.BasicInfo.Units;

public class CreateModel(UnitsApplication unitsApplication) : BasePageModel
{
	[BindProperty]
	public CreateUnitViewModel CreateViewModel { get; set; } = new();
	public List<SelectListItem> BaseUnitList { get; set; } = [];
	public async Task OnGet()
    {
        await FillUnitBaseUnits();
    }

    public async Task<IActionResult> OnPost()
	{
        await FillUnitBaseUnits();
        if (ModelState.IsValid)
		{
			await unitsApplication.CreateAsync(CreateViewModel);
        }
        return RedirectToPage("Index");
	}

    private async Task FillUnitBaseUnits()
    {
        var baseUnitList = await unitsApplication.GetUnits();

        BaseUnitList = baseUnitList.Select(unit => new SelectListItem
        {
            Disabled = false,
            Text = unit.Title,
            Value = unit.Id.ToString()
        })
        .ToList();
		BaseUnitList.Add(new SelectListItem
		{
			Selected = true,
			Text = DataDictionary.BaseUnit,
			Value = Guid.Empty.ToString()
		});
	}
}
