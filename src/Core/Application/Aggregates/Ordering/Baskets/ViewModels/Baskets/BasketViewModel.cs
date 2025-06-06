using Application.Aggregates.Orders.ViewModels.BasketItems;
using Domain.Aggregates.Ordering.Baskets.Enums;
using Framework.DataType;

namespace Application.Aggregates.Ordering.Baskets.ViewModels.Baskets;

public class BasketViewModel
{
    public BasketViewModel()
    {
        BasketItems = new();
    }


    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public string ReferenceNumber { get; set; }
    public Platform Platform { get; set; }
    public BasketStatus BasketStatus { get; set; }
    public Platform Platform { get; set; }
    public string? Description { get; set; }
    public decimal TotalDiscountAmount { get; set; }
    public DiscountType TotalDiscountType { get; set; }
    public decimal SubtotalBeforeBasketDiscount { get; set; }
    public decimal BasketTotal { get; set; }
    public List<BasketItemViewModel> BasketItems { get; set; }

    internal static BasketViewModel FormBasket(Basket basket)
    {
        return new BasketViewModel
        {
            Id = basket.Id,
            ReferenceNumber = basket.ReferenceNumber,
            BasketStatus = basket.BasketStatus,
            TotalDiscountType = basket.TotalDiscountAmount.DiscountType,
            TotalDiscountAmount = basket.TotalDiscountAmount.Value,
            Description = basket.Description,
            Platform = basket.Platform,
            BasketTotal = basket.Total,
            SubtotalBeforeBasketDiscount = basket.TotalBeforeDiscount,
            BasketItems = basket.BasketItems.Select(x => new BasketItemViewModel
            {
                BasePrice = x.ProductAmount.BasePrice,
                DiscountType = x.DiscountAmount.DiscountType,
                DiscountValue = x.DiscountAmount.Value,
                ProductId = x.Product.ProductId,
                ProductName = x.Product.ProductName,
                Quantity = x.ProductAmount.Quantity,
                TotalPrice = x.TotalPrice,
            }).ToList()
        };
    }
}