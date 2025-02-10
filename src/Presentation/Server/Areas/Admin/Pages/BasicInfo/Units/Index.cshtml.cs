using Application.Aggregates.Units;
using Application.Aggregates.Units.ViewModels;
using Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.Units
{
	public class IndexModel(UnitsApplication unitsApplication) : BasePageModel
    {
        public List<UnitViewModel> ViewModel { get; set; } = [];
        public async Task OnGet()
        {
			ViewModel = await unitsApplication.GetUnits();
		}
    }
}
