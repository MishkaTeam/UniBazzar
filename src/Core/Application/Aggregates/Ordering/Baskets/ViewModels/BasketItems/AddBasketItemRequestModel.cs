using Domain.Aggregates.Ordering.Baskets.Enums;

namespace Application.Aggregates.Orders.ViewModels.BasketItems;

public class AddBasketItemRequestModel
{
    public Guid BasketId { get; private set; }
    public long Quantity { get; private set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal BasePrice { get; set; }
    public DiscountType DiscountType { get; set; }
    public decimal DiscountAmount { get; set; }
}