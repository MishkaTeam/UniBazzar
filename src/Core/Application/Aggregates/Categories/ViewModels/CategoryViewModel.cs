using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.Categories.ViewModels;

public class CategoryViewModel : CreateCategoryViewModel
{
	[Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Id))]
	public Guid Id { get; set; }

	[Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.SubCategoriesCount))]
	public int SubCategoriesCount { get; set; }

	public CategoryViewModel? Parent { get; set; }
}