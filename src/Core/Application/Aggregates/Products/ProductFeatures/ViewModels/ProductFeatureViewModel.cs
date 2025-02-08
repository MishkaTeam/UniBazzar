using Application.Aggregates.Products.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.Products.ProductFeatures.ViewModels;

public class ProductFeatureViewModel : CreateProductFeatureViewModel
{
	[Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Id))]
	public Guid Id { get; set; }

	public ProductViewModel? Product { get; set; }
}