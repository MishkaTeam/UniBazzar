using Domain.Aggregates.Ordering.Baskets.Enums;

namespace Application.Aggregates.Orders.ViewModels.BasketItems;

public class BasketItemViewModel
{
    public string ProductName { get; set; }
    public Guid ProductId { get; set; }

    public long Quantity { get; set; }
    public decimal BasePrice { get; set; }
    public decimal TotalPrice { get; set; }

    public decimal DiscountValue { get; set; }
    public DiscountType DiscountType { get; set; }
}