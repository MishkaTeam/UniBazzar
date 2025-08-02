using Application.Aggregates.Ordering.Baskets.ViewModels.BasketItems;
using Domain.Aggregates.Ordering.Baskets.Enums;
using Domain.Aggregates.Ordering.Orders;

namespace Application.Aggregates.Ordering.Orders.ViewModels.OrdersItems;

public class OrderItemViewModel
{
    public OrderItemViewModel()
    {
    }


    public Guid Id { get; set; }
    public string ProductName { get; set; }
    public Guid ProductId { get; set; }
    public long Quantity { get; set; }
    public decimal BasePrice { get; set; }
    public decimal DiscountValue { get; set; }
    public DiscountType DiscountType { get; set; }
    public decimal TotalPrice { get; set; }
    public List<OrderItemAttributeContract> Attributes { get; internal set; }
    public decimal PriceAdjustments { get; internal set; }
    public decimal TotalPriceWithAdjustment { get; internal set; }


    internal static List<OrderItemViewModel> FromOrderItems(List<OrderItem> items)
    {
        return items.Select(x => new OrderItemViewModel
        {
            Id = x.Id,
            BasePrice = x.ProductAmount.BasePrice,
            DiscountType = x.DiscountAmount.DiscountType,
            DiscountValue = x.DiscountAmount.Value,
            ProductId = x.Product.ProductId,
            ProductName = x.Product.ProductName,
            Quantity = x.ProductAmount.Quantity,
            TotalPrice = x.TotalPrice,
            TotalPriceWithAdjustment = x.TotalPriceWithAdjustment,
            PriceAdjustments = x.PriceAdjustments,
            Attributes = OrderItemAttributeContract.FromBasketItemAttribute(x.OrderItemAttribute),
        }).ToList();
    }
}
