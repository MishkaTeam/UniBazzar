using Domain.Aggregates.Products.Enums;

namespace Application.Aggregates.Products.ViewModels;

public class ProductAttributeViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<ProductAttributeValuesViewModel> AttributeValues { get; set; }
    public ProductAttributeType ProductAttributeType { get; set; }
}
