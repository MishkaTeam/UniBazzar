using Domain.Aggregates.Ordering.Baskets.Enums;

namespace Application.Aggregates.Ordering.Baskets.ViewModels.InitializeBasket;

public class InitializeBasketRequestModel
{
    public Platform Platform { get; set; }
    public Guid CustomerId { get; set; }
    public string? Description { get; set; }
    public DiscountType TotalDiscountType { get; set; }
    public decimal TotalDiscountAmount { get; set; }
}