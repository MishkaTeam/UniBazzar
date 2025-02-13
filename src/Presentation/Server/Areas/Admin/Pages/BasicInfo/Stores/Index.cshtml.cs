using Application.Aggregates.Stores;
using Application.Aggregates.Stores.ViewModels;
using Infrastructure;

namespace Server.Areas.Admin.Pages.BasicInfo.Stores;

public class IndexModel
	(StoresApplication storesApplication) : BasePageModel
{
	public List<StoreViewModel> ViewModel { get; set; } = [];

	public async Task OnGetAsync()
	{
		ViewModel =
			await storesApplication.GetStores();
	}
}