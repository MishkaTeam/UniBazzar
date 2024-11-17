namespace Application.Aggregates.Units.ViewModels;

public class CreateUnitViewModel
{
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Title))]
	public string Title { get; set; }

	[System.ComponentModel.DataAnnotations.Display
	(ResourceType = typeof(Resources.DataDictionary),
	Name = nameof(Resources.DataDictionary.BaseUnit))]
	public Guid? BaseUnitId { get; set; }

	[System.ComponentModel.DataAnnotations.Display
	(ResourceType = typeof(Resources.DataDictionary),
	Name = nameof(Resources.DataDictionary.UnitRatio))]
	public decimal Ratio { get; set; }
}
