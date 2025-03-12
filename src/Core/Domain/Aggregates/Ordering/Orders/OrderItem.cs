using Domain.Aggregates.Ordering.ValueObjects;
using Domain.BuildingBlocks.Aggregates;

namespace Domain.Aggregates.Ordering.Orders;

public class OrderItem : Entity
{
    public Guid OrderId { get; private set; }
    public long OrderReferenceNumber { get; private set; }
    public ProductType Product { get; private set; }
    public ProductAmount ProductAmount { get; private set; }
    public DiscountAmount DiscountAmount { get; private set; }

    public OrderItem(Guid orderId, long orderReferenceNumber, ProductType product, ProductAmount productAmount,
        DiscountAmount discountAmount)
    {
        OrderId = orderId;
        OrderReferenceNumber = orderReferenceNumber;
        Product = product;
        ProductAmount = productAmount;
        DiscountAmount = discountAmount;
    }
}