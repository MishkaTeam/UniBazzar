using BuildingBlocks.Domain.Aggregates;

namespace Domain.Aggregates.Attributes;

public class AttributeValues : Entity
{
    public string Name { get; private set; }
    public decimal PriceAdjustment { get; private set; }
    public decimal WeightAdjustment { get; private set; }
    public bool IsPreSelected { get; private set; }
}
