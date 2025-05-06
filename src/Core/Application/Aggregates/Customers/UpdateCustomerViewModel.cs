using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.Customers;

public class UpdateCustomerViewModel : CreateCustomerViewModel
{
	public UpdateCustomerViewModel()
	{
	}


	[Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Id))]
	public Guid Id { get; set; }
}