using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.Products.ViewModels;

public class ProductViewModel : UpdateProductViewModel
{
	[Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.SKU))]
	public string SKU { get; set; }

    [Display
    (ResourceType = typeof(Resources.DataDictionary),
    Name = nameof(Resources.DataDictionary.Slug))]
    public string Slug { get; set; }
}