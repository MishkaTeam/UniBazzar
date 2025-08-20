using Application.Aggregates.Ordering.Orders;
using Application.Aggregates.Ordering.Orders.ViewModels.Orders;
using BuildingBlocks.Domain.Context;
using Infrastructure;

namespace Server.Pages
{
    public class ThankyouModel(IExecutionContextAccessor executionContext, OrderApplication orderApplication) : BasePageModel
    {
        public OrderViewModel Order { get; set; } = new();
        public async Task OnGetAsync(Guid orderId)
        {
            var orderResult = await orderApplication.GetOrderAsync(orderId);
            if (orderResult is { IsSuccessful: false }
            || orderResult.Data == null)
                return;

            if (orderResult.Data.CustomerId != executionContext.UserId)
                return;

            Order = orderResult.Data;
        }
    }
}
