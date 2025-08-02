using BuildingBlocks.Domain.Aggregates;

namespace Domain.Aggregates.Ordering.Baskets;

public class OrderItemAttribute : Entity
{
    protected OrderItemAttribute()
    {
        
    }
    private OrderItemAttribute(Guid productAttributeId,
                                string productAttributeName,
                                string productAttributeValue,
                                decimal priceAdjustment,
                                Guid? productAttributeValueId)
    {
        ProductAttributeId = productAttributeId;
        ProductAttributeName = productAttributeName;
        ProductAttributeValue = productAttributeValue;
        PriceAdjustment = priceAdjustment;
        ProductAttributeValueId = productAttributeValueId;
    }

    public Guid BasketItemId { get; private set; }
    public Guid ProductAttributeId { get; private set; }
    public string ProductAttributeName { get; private set; }
    public Guid? ProductAttributeValueId { get; private set; }
    public string ProductAttributeValue { get; private set; }
    public decimal PriceAdjustment { get; private set; }

    public static OrderItemAttribute Create(Guid productAttributeId,
                                             string productAttributeName,
                                             string productAttributeValue,
                                             decimal priceAdjustment,
                                             Guid? productAttributeValueId)
    {
        var attribute = new OrderItemAttribute(
                productAttributeId,
                productAttributeName,
                productAttributeValue,
                priceAdjustment,
                productAttributeValueId);

        return attribute;
    }
}


