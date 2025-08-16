using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.Products.ProductFeatures.ViewModels;

public class CreateProductFeatureViewModel
{
	public CreateProductFeatureViewModel()
	{
		Order = 10;
	}


	[Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.ProductId))]
	public Guid ProductId { get; set; }

	[Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Name))]
	public string Key { get; set; }

	[Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Value))]
	public string Value { get; set; }

	[Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsPinned))]
	public bool IsPinned { get; set; }

	[Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Order))]
	public int Order { get; set; }
}