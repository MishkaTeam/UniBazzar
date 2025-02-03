using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.Categories.ViewModels;

public class CategoryViewModel : CreateCategoryViewModel
{
	[Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Id))]
	public Guid Id { get; set; }
}