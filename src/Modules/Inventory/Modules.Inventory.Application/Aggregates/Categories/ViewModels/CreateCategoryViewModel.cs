using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.Categories.ViewModels;

public class CreateCategoryViewModel
{
	public CreateCategoryViewModel()
	{
		IconClass = "bi bi-box-fill";
	}


	[Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Name))]
	public string Name { get; set; }

	[Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Parent))]
	public Guid? ParentId { get; set; }

	[Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IconClass))]
	public string IconClass { get; set; }
}