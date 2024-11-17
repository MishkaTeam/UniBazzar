namespace Application.Aggregates.Units.ViewModels;

public class UnitViewModel : CreateUnitViewModel
{

	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Id))]
	public Guid Id { get; set; }

	[System.ComponentModel.DataAnnotations.Display
	(ResourceType = typeof(Resources.DataDictionary),
	Name = nameof(Resources.DataDictionary.BaseUnit))]
	public UnitViewModel? BaseUnit { get; set; }
}
