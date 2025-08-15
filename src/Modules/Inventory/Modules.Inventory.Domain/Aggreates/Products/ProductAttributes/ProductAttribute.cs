using BuildingBlocks.Domain.Aggregates;
using Domain.Aggregates.Attributes;
using Domain.Aggregates.Products.Enums;
using Attribute = Domain.Aggregates.Attributes.Attribute;

namespace Domain.Aggregates.Products.ProductAttributes;

public class ProductAttribute : Entity
{
    public Guid AttributeId { get; private set; }
    public Attribute Attribute { get; private set; }
    public ProductAttributeType ProductAttributeType { get; private set; }
}
