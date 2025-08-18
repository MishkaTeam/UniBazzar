using Application.Aggregates.Ordering.Orders;
using Application.Aggregates.Ordering.Orders.ViewModels.OrdersItems;
using Infrastructure;

namespace Server.Areas.Admin.Pages.BasicInfo.Orders.OrderItems;

public class IndexModel(OrderApplication orderApplication) : BasePageModel
{
    public List<OrderItemViewModel> viewModel { get; set; }
    public async Task OnGetAsync(Guid orderId)
    {
        viewModel = (await orderApplication.GetOrderItems(orderId)).Data!;
    }
}
