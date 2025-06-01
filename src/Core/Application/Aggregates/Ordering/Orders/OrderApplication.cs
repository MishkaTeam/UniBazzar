using Application.Aggregates.Customers;
using Application.Aggregates.Ordering.Orders.ProcessOrder;
using Domain;
using Domain.Aggregates.Ordering.Baskets.Data;
using Domain.Aggregates.Ordering.Orders;
using Domain.Aggregates.Ordering.Orders.Data;
using Framework.DataType;
using Modules.Treasury.Api.TreasuryAbstraction;
using Modules.Treasury.Application.Contracts;

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
                return (ErrorType.NotFound, string.Format(Resources.Messages.Errors.NotFound, Resources.DataDictionary.Basket));

            var order = Order.CreateFromBasket(basket);
            await orderRepository.AddAsync(order, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var customer = await customerApplication.GetCustomerAsync(basket.OwnerId);
            var recCustomer = new ReceiptCustomer(customer.Id, string.Join(customer.FirstName, ' ', customer.LastName));
            var receiptRes = receipts.CreateCashReceiptAsync(recCustomer, basket.Total, cancellationToken);

            return new ProcessOrderResponseModel
            {
                OrderId = order.Id,
                OrderReferenceNumber = order.ReferenceNumber,
            };
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}