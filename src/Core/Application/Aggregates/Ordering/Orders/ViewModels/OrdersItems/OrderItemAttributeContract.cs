using Domain.Aggregates.Ordering.Baskets;

namespace Application.Aggregates.Ordering.Orders.ViewModels.OrdersItems;

public class OrderItemAttributeContract
{
    public Guid ProductAttributeId { get; set; }
    public string ProductAttributeName { get; set; }
    public Guid? ProductAttributeValueId { get; set; }
    public string ProductAttributeValue { get; set; }
    public decimal PriceAdjustment { get; set; }

    public static List<OrderItemAttributeContract> FromBasketItemAttribute(List<OrderItemAttribute>? orderItemAttribute)
    {
        if (orderItemAttribute == null)
            return [];

        return [.. orderItemAttribute.Select(b => new OrderItemAttributeContract
        {

            ProductAttributeName = b.ProductAttributeName,
            ProductAttributeValueId = b.ProductAttributeValueId,
            PriceAdjustment = b.PriceAdjustment,
            ProductAttributeId = b.ProductAttributeId,
            ProductAttributeValue = b.ProductAttributeValue
        })];
    }
}