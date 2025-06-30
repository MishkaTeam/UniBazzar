using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.Discounts.ViewModels;

public class DetailsAndDeleteDiscountProductViewModel : DiscountProductViewModel
{
	public Guid Id { get; set; }

}
