using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.Stores.ViewModels;

public class CreateStoreViewModel
{
	public CreateStoreViewModel()
	{
		Culture = "fa-IR";
		IsActive = true;
	}


	[Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Name))]
	public string Name { get; set; }

	[Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.FullDescription))]
	public string? Description { get; set; }

	[Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.CellPhonenumber))]
	[RegularExpression
		(Constants.RegularExpression.CellPhoneNumber)]
	public string PhoneNumber { get; set; }

	[Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Address))]
	public string Address { get; set; }

	[Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.culture))]
	public string? Culture { get; set; }

	[Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Logo))]
	public string? LogoUrl { get; set; }

	[Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsActive))]
	public bool IsActive { get; set; }
}