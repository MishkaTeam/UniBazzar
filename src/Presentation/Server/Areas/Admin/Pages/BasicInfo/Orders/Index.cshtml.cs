using Application.Aggregates.Ordering.Orders;
using Application.Aggregates.Ordering.Orders.ViewModels.Orders;
using Infrastructure;

namespace Server.Areas.Admin.Pages.BasicInfo.Orders;

public class IndexModel
    (OrderApplication orderApplication) : BasePageModel
{
    public List<OrderViewModel> ViewModel { get; set; } = [];

    public async Task OnGetAsync()
    {
        ViewModel =
            (await orderApplication.GetAllOrderAsync()).Data!;
    }
}
