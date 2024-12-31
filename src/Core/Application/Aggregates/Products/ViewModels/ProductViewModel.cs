using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.Products.ViewModels;

public class ProductViewModel : CreateProductViewModel
{
	[Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Id))]
	public Guid Id { get; set; }
}