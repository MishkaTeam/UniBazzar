using Domain.Aggregates.Ordering.ValueObjects;
using BuildingBlocks.Domain.Aggregates;

namespace Domain.Aggregates.Ordering.Orders;

public class OrderItem : Entity
{
    public Guid OrderId { get; private set; }
    public string OrderReferenceNumber { get; private set; }
    public ProductType Product { get; private set; }
    public ProductAmount ProductAmount { get; private set; }
    public DiscountAmount DiscountAmount { get; private set; }

    private OrderItem()
    {
        //FOR EF!
    }

    public OrderItem(Guid orderId, string orderReferenceNumber, ProductType product, ProductAmount productAmount,
        DiscountAmount discountAmount)
    {
        OrderId = orderId;
        OrderReferenceNumber = orderReferenceNumber;
        Product = product;
        ProductAmount = productAmount;
        DiscountAmount = discountAmount;
    }
}