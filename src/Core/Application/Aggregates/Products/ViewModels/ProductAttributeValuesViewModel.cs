namespace Application.Aggregates.Products.ViewModels;

public class ProductAttributeValuesViewModel
{
    public string Name { get; set; }
    public decimal PriceAdjustment { get; set; }
    public decimal WeightAdjustment { get; set; }
    public bool IsPreSelected { get; set; }

}
