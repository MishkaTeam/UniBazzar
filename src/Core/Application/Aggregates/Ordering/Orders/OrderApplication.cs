using Application.Aggregates.Customers;
using Application.Aggregates.Ordering.Orders.ProcessOrder;
using Application.Aggregates.Ordering.Orders.ViewModels.Orders;
using Application.Aggregates.Ordering.Orders.ViewModels.OrdersItems;
using Domain;
using Domain.Aggregates.Ordering.Baskets.Data;
using Domain.Aggregates.Ordering.Orders;
using Domain.Aggregates.Ordering.Orders.Data;
using Framework.DataType;
using Mapster;
using Modules.Treasury.Api.TreasuryAbstraction;
using Modules.Treasury.Application.Contracts;
using Resources;
using Resources.Messages;

namespace Application.Aggregates.Ordering.Orders;

public class OrderApplication(
    IBasketRepository basketRepository,
    IOrderRepository orderRepository,
    IReceiptsApi receipts,
    CustomerApplication customerApplication,
    IUnitOfWork unitOfWork)
{
    public async Task<ResultContract<ProcessOrderResponseModel>> ProcessOrderRequest(ProcessOrderRequestModel request, CancellationToken cancellationToken)
    {
        try
        {
            var basket = await basketRepository.GetWithItemsByIdAsync(request.BasketId);

            if (basket == null)
            {
                var message =
                    string.Format(Errors.NotFound, DataDictionary.Basket);

                return (ErrorType.NotFound, message);
            }

            if (basket.CustomerId == null ||
                basket.CustomerId.Value == Guid.Empty)
            {
                var message =
                    string.Format(Errors.NotFound, DataDictionary.Customer);

                return (ErrorType.NotFound, message);
            }

            var order =
                Order.CreateFromBasket(basket);

            await orderRepository.AddAsync(order, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var customer =
                await customerApplication.GetCustomerAsync(basket.CustomerId!.Value);

            var recCustomer = new ReceiptCustomer
                (customer.Id, string.Join(customer.FirstName, ' ', customer.LastName));

            var receiptRes = receipts.CreateCashReceiptAsync
                (customer: recCustomer, price: basket.Total, orderId: order.Id, cancellationToken: cancellationToken);

            return new ProcessOrderResponseModel
            {
                OrderId = order.Id,
                OrderReferenceNumber = order.ReferenceNumber,
            };
        }
        catch (Exception ex)
        {
            return (ErrorType.InternalError, ex.Message);
        }
    }

    public async Task<ResultContract<List<OrderViewModel>>> GetAllOrderAsync()
    {
        var orders =
            (await orderRepository.GetAllAsync())
            .OrderByDescending(x => x.InsertDateTime)
            .ToList();

        return OrderViewModel.FromOrderList(orders);
    }

    public async Task<ResultContract<List<OrderItemViewModel>>> GetOrderItems(Guid orderId)
    {
        var orderItems =
            await orderRepository.GetAllOrderItem(orderId);

        return OrderItemViewModel.FromOrderItems(orderItems);
    }
}