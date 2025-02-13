using Application.Aggregates.Units;
using Application.Aggregates.Units.ViewModels;
using Infrastructure;

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
