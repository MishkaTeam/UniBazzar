using BuildingBlocks.Domain.Aggregates;
using Domain.Aggregates.Categories;

namespace Domain.Aggregates.Attributes;

public class Attribute : Entity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; }
    public List<AttributeValues> AttributeValues { get; private set; }
}
