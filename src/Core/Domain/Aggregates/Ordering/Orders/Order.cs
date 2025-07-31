using Domain.Aggregates.Ordering.Baskets;
using Domain.Aggregates.Ordering.Baskets.Enums;
using Domain.Aggregates.Ordering.ValueObjects;
using Entity = BuildingBlocks.Domain.Aggregates.Entity;

namespace Domain.Aggregates.Ordering.Orders;

public class Order : Entity
{
    public string ReferenceNumber { get; private set; }
    public Platform Platform { get; private set; }
    public string? Description { get; private set; }
    public DiscountAmount TotalDiscountAmount { get; private set; }
    public Guid BasketId { get; private set; }
    public string BasketReferenceNumber { get; private set; }

    public List<OrderItem> OrderItems { get; private set; }


    public decimal TotalWithoutDiscount
    {
        get
        {
            return OrderItems.Sum(x => x.TotalBeforeDiscount);
        }
    }

    public decimal TotalBeforeDiscount
    {
        get
        {
            return OrderItems.Sum(x => x.TotalPriceWithAdjustment);
        }
    }

    public decimal TotalItemDiscounts
    {
        get
        {
            return OrderItems.Sum(x => x.DiscountPrice);
        }
    }


    public decimal Total
    {
        get
        {
            return TotalDiscountAmount.ApplyDiscount(TotalBeforeDiscount);
        }
    }

    protected Order()
    {
        //FOR EF!
    }

    public Order(Guid basketId, string basketReferenceNumber)
    {
        BasketId = basketId;
        BasketReferenceNumber = basketReferenceNumber;
    }

    public void AddOrderItem(List<OrderItem> orderItems)
    {
        OrderItems = orderItems;
    }


    public static Order CreateFromBasket(Basket? basket)
    {
        ArgumentNullException.ThrowIfNull(basket);

        if (basket.BasketItems.Count == 0)
            throw new InvalidDataException("Basket is empty");

        if (basket.BasketStatus != BasketStatus.CHECKOUT)
            throw new Exception("Basket is not in Check out");

        var order = new Order(basket.Id, basket.ReferenceNumber);
        order.AddOrderItem(basket.BasketItems
            .Select(x => new OrderItem
            (
                order.Id,
                order.ReferenceNumber,
                x.Product,
                x.ProductAmount,
                x.DiscountAmount))
            .ToList());

        return order;
    }
}