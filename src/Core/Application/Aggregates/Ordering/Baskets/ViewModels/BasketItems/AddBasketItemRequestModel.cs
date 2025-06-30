using Domain.Aggregates.Ordering.Baskets;
using Domain.Aggregates.Ordering.Baskets.Enums;

namespace Application.Aggregates.Ordering.Baskets.ViewModels.BasketItems;

public class AddBasketItemRequestModel
{
    public Guid BasketId { get; set; }
    public long Quantity { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal BasePrice { get; set; }
    public DiscountType DiscountType { get; set; }
    public decimal DiscountAmount { get; set; }
    public List<BasketItemAttributeContract>? BasketItemAttributes { get; set; }

    internal List<BasketItemAttribute> ToBasketItemAttribute()
    {
        var result = new List<BasketItemAttribute>();

        foreach (var attribute in BasketItemAttributes ?? [])
        {
            result.Add(BasketItemAttribute.Create(
                attribute.ProductAttributeId, 
                attribute.ProductAttributeName, 
                attribute.ProductAttributeValue, 
                attribute.PriceAdjustment,
                attribute.ProductAttributeValueId));
        }
        return result;
    }
}
