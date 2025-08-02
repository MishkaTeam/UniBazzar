using BuildingBlocks.Domain.Aggregates;
using Domain.Aggregates.Ordering.Baskets;
using Domain.Aggregates.Ordering.ValueObjects;

namespace Domain.Aggregates.Ordering.Orders;

public class OrderItem : Entity
{
    public Guid OrderId { get; private set; }
    public string OrderReferenceNumber { get; private set; }
    public ProductType Product { get; private set; }
    public ProductAmount ProductAmount { get; private set; }
    public DiscountAmount DiscountAmount { get; private set; }

    public List<OrderItemAttribute>? OrderItemAttribute { get; private set; }

    public decimal TotalPriceWithAdjustment =>
        DiscountAmount.ApplyDiscount(ProductAmount.TotalPrice) + PriceAdjustments;

    public decimal PriceAdjustments =>
        (OrderItemAttribute?.Sum(x => x.PriceAdjustment) ?? 0);

    public decimal TotalPrice =>
        DiscountAmount.ApplyDiscount(ProductAmount.TotalPrice, ProductAmount.Quantity);

    public decimal TotalBeforeDiscount =>
        ProductAmount.TotalPrice;

    public decimal DiscountPrice =>
        DiscountAmount.ConvertToPrice(ProductAmount.TotalPrice, ProductAmount.Quantity);

    protected OrderItem()
    {
        // FOR EF!
    }

    public OrderItem(Guid orderId, string orderReferenceNumber, ProductType product,
        ProductAmount productAmount, DiscountAmount discountAmount)
    {
        OrderId = orderId;
        OrderReferenceNumber = orderReferenceNumber;
        Product = product;
        ProductAmount = productAmount;
        DiscountAmount = discountAmount;
        OrderItemAttribute = new(); // Ã·Êê?—? «“ null
    }
}
