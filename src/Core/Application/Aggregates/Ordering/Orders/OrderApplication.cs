using Application.Aggregates.Ordering.Orders.ProcessOrder;
using Domain;
using Domain.Aggregates.Ordering.Baskets.Data;
using Domain.Aggregates.Ordering.Orders;
using Domain.Aggregates.Ordering.Orders.Data;
using Framework.DataType;

namespace Application.Aggregates.Ordering.Orders;

public class OrderApplication(
    IBasketRepository basketRepository,
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork)
{
    public async Task<ResultContract<ProcessOrderResponseModel>> ProcessOrderRequest(ProcessOrderRequestModel request)
    {
        var basket = await basketRepository.GetWithItemsByIdAsync(request.BasketId);
        var order = Order.CreateFromBasket(basket);
        await orderRepository.AddAsync(order);
        await unitOfWork.CommitAsync();
        return new ProcessOrderResponseModel
        {
            OrderId = order.Id,
            OrderReferenceNumber = order.ReferenceNumber,
        };
    }
}