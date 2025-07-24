using Application.Aggregates.HomeViews;
using Application.Aggregates.HomeViews.ViewModels;
using Infrastructure;

namespace Server.Areas.Admin.Pages.BasicInfo.HomeViews;

public class IndexModel
    (HomeViewsApplication homeViewsApplication) : BasePageModel
{
    public List<HomeViewViewModel> ViewModel { get; set; } = [];

    public async Task OnGetAsync()
    {
        ViewModel =
            (await homeViewsApplication.GetHomeViews()).Data!;
    }
}
