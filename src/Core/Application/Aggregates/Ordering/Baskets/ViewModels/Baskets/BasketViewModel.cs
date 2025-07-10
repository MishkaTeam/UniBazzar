using Application.Aggregates.Ordering.Baskets.ViewModels.BasketItems;
using Domain.Aggregates.Ordering.Baskets;
using Domain.Aggregates.Ordering.Baskets.Enums;
using Framework.DataType;

namespace Application.Aggregates.Ordering.Baskets.ViewModels.Baskets;

public class BasketViewModel
{
    public BasketViewModel()
    {
        BasketItems = new();

        TotalDiscountType =
            DiscountType.Price;
    }


    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public string ReferenceNumber { get; set; }
    public Platform Platform { get; set; }
    public BasketStatus BasketStatus { get; set; }
    public string? Description { get; set; }
    public decimal TotalDiscountAmount { get; set; }
    public DiscountType TotalDiscountType { get; set; }
    public decimal TotalWithoutDiscount { get; set; }
    public decimal SubtotalBeforeBasketDiscount { get; set; }
    public decimal BasketTotal { get; set; }
    public List<BasketItemViewModel> BasketItems { get; set; }
    public decimal TotalItemDiscounts { get; set; }

    internal static BasketViewModel FromBasket(Basket basket)
    {
        return new BasketViewModel
        {
            Id = basket.Id,
            OwnerId = basket.OwnerId,
            ReferenceNumber = basket.ReferenceNumber,
            BasketStatus = basket.BasketStatus,
            TotalDiscountType = basket.TotalDiscountAmount.DiscountType,
            TotalDiscountAmount = basket.TotalDiscountAmount.Value,
            Description = basket.Description,
            Platform = basket.Platform,
            BasketTotal = basket.Total,
            TotalWithoutDiscount = basket.TotalWithoutDiscount,
            SubtotalBeforeBasketDiscount = basket.TotalBeforeDiscount,
            TotalItemDiscounts = basket.TotalItemDiscounts,
            BasketItems = basket.BasketItems.Select(x => new BasketItemViewModel
            {
                Id = x.Id,
                BasePrice = x.ProductAmount.BasePrice,
                DiscountType = x.DiscountAmount.DiscountType,
                DiscountValue = x.DiscountAmount.Value,
                ProductId = x.Product.ProductId,
                ProductName = x.Product.ProductName,
                Quantity = x.ProductAmount.Quantity,
                TotalPrice = x.TotalPrice,
                Attributes = BasketItemAttributeContract.FromBasketItemAttribute(x.BasketItemAttributes),
            }).ToList()
        };
    }
}