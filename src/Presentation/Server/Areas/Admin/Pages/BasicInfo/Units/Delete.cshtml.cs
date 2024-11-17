using Application.Aggregates.Units.ViewModels;
using Application.Aggregates.Units;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Resources;

namespace Server.Areas.Admin.Pages.BasicInfo.Units
{
    public class DeleteModel(UnitsApplication unitsApplication) : PageModel
    {
		[BindProperty]
		public UnitViewModel DeleteViewModel { get; set; } = new();
		public List<SelectListItem> BaseUnitList { get; set; } = [];

		public async Task OnGet(Guid Id)
		{
			DeleteViewModel = await unitsApplication.GetUnitAsync(Id);
			await FillUnitBaseUnits();
		}

		public async Task<IActionResult> OnPost()
		{
			await unitsApplication.DeleteAsync(DeleteViewModel.Id);
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
}

