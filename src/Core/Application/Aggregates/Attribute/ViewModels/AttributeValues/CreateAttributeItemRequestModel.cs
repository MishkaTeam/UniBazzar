namespace Application.Aggregates.Attribute.ViewModels.AttributeValues;

public class CreateAttributeItemRequestModel
{
    public Guid AttributeId { get; set; }
    public string Name { get; set; }
    public decimal PriceAdjustment { get; set; }
    public decimal WeightAdjustment { get; set; }
    public bool IsPreSelected { get; set; }
}
