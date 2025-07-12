using Application.Aggregates.Attribute.ViewModels.AttributeValues;

namespace Application.Aggregates.Attribute.ViewModels;

public class AttributeViewModel : UpdateAttributeViewModel
{
    public AttributeViewModel()
    {

    }
    public List<AttributeValueViewModel> attributeValues { get; set; }
    internal static AttributeViewModel FromAttribute(Domain.Aggregates.Attributes.Attribute attribute)
    {
        return new AttributeViewModel
        {
            Id = attribute.Id,
            Name = attribute.Name,
            CategoryId = attribute.CategoryId,
            Description = attribute.Description,
            attributeValues = attribute.AttributeValues.Select(x => new AttributeValueViewModel
            {
                Name = x.Name,
                PriceAdjustment = x.PriceAdjustment,
                WeightAdjustment = x.WeightAdjustment,
                IsPreSelected = x.IsPreSelected
            }).ToList(),
        };
    }
}
