using Domain.Aggregates.Ordering.Baskets.Enums;

namespace Application.Aggregates.Orders.ViewModels;

public class InitializeBasketRequestModel
{
    public Platform platform { get; set; }
    public string? Description { get; set; }
    public DiscountType TotalDiscountType { get; set; }
    public decimal TotalDiscountAmount { get; set; }

}