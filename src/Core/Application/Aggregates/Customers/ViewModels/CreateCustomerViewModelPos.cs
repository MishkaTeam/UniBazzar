using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace Application.Aggregates.Customers.ViewModels;

public class CreateCustomerViewModelPos
{
	public CreateCustomerViewModelPos()
	{	
	}


	[Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Family))]
	[Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public string LastName { get; set; }

	[Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.NationalCode))]
	[RegularExpression
		(Constants.RegularExpression.NationalCode,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.NationalCode))]
	public string? NationalCode { get; set; }

	[Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Mobile))]
	[Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	[RegularExpression
		(Constants.RegularExpression.CellPhoneNumber,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.CellPhoneNumber))]
	public string Mobile { get; set; }
}