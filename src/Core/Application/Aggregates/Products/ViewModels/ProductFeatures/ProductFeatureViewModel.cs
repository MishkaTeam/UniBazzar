using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.Products.ViewModels.ProductFeatures;

public class ProductFeatureViewModel : CreateProductFeatureViewModel
{
	[Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Id))]
	public Guid Id { get; set; }
}