using BuildingBlocks.Domain.Aggregates;

namespace Domain.Aggregates.Attributes;

public class AttributeValues : Entity
{
    public AttributeValues()
    {

    }

    public string Name { get; private set; }
    public decimal PriceAdjustment { get; private set; }
    public decimal WeightAdjustment { get; private set; }
    public bool IsPreSelected { get; private set; }


    public static AttributeValues Create(string name, decimal priceAdjustment, decimal weightAdjustment, bool isPreSelected)
    {
        var attributes = new AttributeValues(name, priceAdjustment, weightAdjustment, isPreSelected)
        {
            Name = name,
            PriceAdjustment = priceAdjustment,
            WeightAdjustment = weightAdjustment,
            IsPreSelected = isPreSelected
        };
        return attributes;
    }
    public void Update(string name, decimal priceAdjustment, decimal weightAdjustment, bool isPreSelected)
    {
        Name = name;
        PriceAdjustment = priceAdjustment;
        WeightAdjustment = weightAdjustment;
        IsPreSelected = isPreSelected;
    }
    private AttributeValues(string name, decimal priceAdjustment, decimal weightAdjustment, bool isPreSelected)
    {
        Name = name;
        PriceAdjustment = priceAdjustment;
        WeightAdjustment = weightAdjustment;
        IsPreSelected = isPreSelected;
    }
}
