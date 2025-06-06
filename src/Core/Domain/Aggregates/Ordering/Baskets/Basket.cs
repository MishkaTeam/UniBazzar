using Domain.Aggregates.Ordering.Baskets.Enums;
using Domain.Aggregates.Ordering.ValueObjects;
using Entity = BuildingBlocks.Domain.Aggregates.Entity;

namespace Domain.Aggregates.Ordering.Baskets;

public class Basket : Entity
{
    public string ReferenceNumber { get; private set; }
    public BasketStatus BasketStatus { get; private set; }
    public Platform Platform { get; private set; }
    public string? Description { get; private set; }
    public DiscountAmount TotalDiscountAmount { get; private set; }

    public List<BasketItem> BasketItems  { get; private set; }


    public decimal TotalBeforeDiscount
    {
        get
        {
            return BasketItems.Sum(x => x.TotalPrice);
        }
    }

    public decimal Total
    {
        get
        {
            TotalDiscountAmount ??= DiscountAmount.CreateNoDiscount();

            return TotalDiscountAmount.ApplyDiscount(TotalBeforeDiscount);
        }
    }

    protected Basket()
    {
        // FOR EF!
    }

    private Basket(Platform platform)
    {
        Platform = platform;
        BasketItems = new List<BasketItem>();

        SetOwner(ownerId);

        // For test
        ReferenceNumber = Guid.NewGuid().ToString();
    }

    public static Basket Initialize(Platform platform)
    {
        var basket = new Basket(platform);
        return basket;
    }

    public void AddItem(BasketItem basketItem)
    {
        BasketItems.Add(basketItem);
    }

    public void SetDescription(string description) => Description = description;
    public void SetTotalDiscount(decimal amount, DiscountType type) => TotalDiscountAmount = DiscountAmount.Create(amount, type);

    public void Checkout()
    {
        BasketStatus = BasketStatus.CHECKOUT;
    }
}