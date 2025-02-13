using Application.Aggregates.Units;
using Application.Aggregates.Units.ViewModels;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Resources;

namespace Server.Areas.Admin.Pages.BasicInfo.Units
{
	public class UpdateModel(UnitsApplication unitsApplication) : BasePageModel
    {
		[BindProperty]
		public UnitViewModel UpdateViewModel { get; set; } = new();
		public List<SelectListItem> BaseUnitList { get; set; } = [];

		public async Task OnGet(Guid Id)
		{
			UpdateViewModel = await unitsApplication.GetUnitAsync(Id);
			 await FillBaseUnits();
		}

		public async Task<IActionResult> OnPost()
		{
			await FillBaseUnits();
			if (ModelState.IsValid)
			{
				await unitsApplication.UpdateAsync(UpdateViewModel);
			}
			return RedirectToPage("Index");

		}

		private async Task FillBaseUnits()
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
}
