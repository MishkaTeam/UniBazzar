using Domain.Aggregates.Ordering.Baskets;

namespace Application.Aggregates.Ordering.Baskets.ViewModels.BasketItems;

public class BasketItemAttributeContract
{
    public Guid ProductAttributeId { get; set; }
    public string ProductAttributeName { get; set; }
    public Guid? ProductAttributeValueId { get; set; }
    public string ProductAttributeValue { get; set; }
    public decimal PriceAdjustment { get; set; }

    public static List<BasketItemAttributeContract> FromBasketItemAttribute(List<BasketItemAttribute>? basketItemAttribute)
    {
        if (basketItemAttribute == null)
            return [];

        return [.. basketItemAttribute.Select(b => new BasketItemAttributeContract
        {
            
            ProductAttributeName = b.ProductAttributeName,
            ProductAttributeValueId = b.ProductAttributeValueId,
            PriceAdjustment = b.PriceAdjustment,
            ProductAttributeId = b.ProductAttributeId,
            ProductAttributeValue = b.ProductAttributeValue
        })];

    }
}