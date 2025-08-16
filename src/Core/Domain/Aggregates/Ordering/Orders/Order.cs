using Domain.Aggregates.Customers;
using Domain.Aggregates.Ordering.Baskets;
using Domain.Aggregates.Ordering.Baskets.Enums;
using Domain.Aggregates.Ordering.ValueObjects;
using Framework.DataType;
using Resources;
using Resources.Messages;
using Entity = BuildingBlocks.Domain.Aggregates.Entity;

namespace Domain.Aggregates.Ordering.Orders;

public class Order : Entity
{
    protected Order()
    {
        //FOR EF!
    }

    public Order(Guid basketId, string basketReferenceNumber, Guid customerId)
    {
        BasketId = basketId;
        BasketReferenceNumber = basketReferenceNumber;
        CustomerId = customerId;

        // For test
        ReferenceNumber = Guid.NewGuid().ToString();
    }


    public Guid CustomerId { get; private set; }
    public string ReferenceNumber { get; private set; }
    public Platform Platform { get; private set; }
    public string? Description { get; private set; }
    public DiscountAmount TotalDiscountAmount { get; private set; }
    public Guid BasketId { get; private set; }
    public string BasketReferenceNumber { get; private set; }

    public Customer Customer { get; private set; }
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

        if (basket.CustomerId == null ||
            basket.CustomerId.Value == Guid.Empty)
        {
            throw new Exception("Basket have not a Customer");
        }

        var order = new Order
            (basket.Id, basket.ReferenceNumber, basket.CustomerId.Value!);

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