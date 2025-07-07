namespace Application.Aggregates.Ordering.Baskets.ViewModels.BasketItems;

public class BasketItemAttributeContract
{
    public Guid ProductAttributeId { get; set; }
    public string ProductAttributeName { get; set; }
    public Guid? ProductAttributeValueId { get; set; }
    public string ProductAttributeValue { get; set; }
    public decimal PriceAdjustment { get; set; }
}