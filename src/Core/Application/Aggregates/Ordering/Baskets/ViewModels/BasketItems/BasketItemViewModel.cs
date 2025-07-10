using Domain.Aggregates.Ordering.Baskets.Enums;

namespace Application.Aggregates.Ordering.Baskets.ViewModels.BasketItems;

public class BasketItemViewModel
{
    public Guid Id { get; set; }
    public string ProductName { get; set; }
    public Guid ProductId { get; set; }
    public long Quantity { get; set; }
    public decimal BasePrice { get; set; }
    public decimal DiscountValue { get; set; }
    public DiscountType DiscountType { get; set; }
    public decimal TotalPrice { get; set; }
    public List<BasketItemAttributeContract> Attributes { get; internal set; }
}