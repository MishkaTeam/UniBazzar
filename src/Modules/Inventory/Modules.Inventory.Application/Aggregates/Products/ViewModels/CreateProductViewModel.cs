using System.ComponentModel.DataAnnotations;
using Domain.Aggregates.Products.Enums;

namespace Application.Aggregates.Products.ViewModels;

public class CreateProductViewModel
{
	[Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Name))]
	public string Name { get; set; }

	[Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.ShortDescription))]
	public string ShortDescription { get; set; }

	[Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.FullDescription))]
	public string FullDescription { get; set; }

	[Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.StoreId))]
	public Guid StoreId { get; set; }

	[Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.CategoryId))]
	public Guid CategoryId { get; set; }

	[Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Unit))]
	public Guid UnitId { get; set; }

	[Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.ProductType))]
	public ProductType ProductType { get; set; }

	[Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.DownloadUrl))]
	public string? DownloadUrl { get; set; }
}