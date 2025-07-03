using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.Discounts.ViewModels;

public class DetailsAndDeleteDiscountProductViewModel : DiscountProductViewModel
{
	public Guid Id { get; set; }

	[Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Name))]
	public string? ProductName { get; set; }

}
