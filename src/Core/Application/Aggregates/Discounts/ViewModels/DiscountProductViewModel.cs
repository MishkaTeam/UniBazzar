using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.Discounts.ViewModels;

public class DiscountProductViewModel
{
	public Guid ProductId { get; set; }
	public Guid DiscountId { get; set; }
}
