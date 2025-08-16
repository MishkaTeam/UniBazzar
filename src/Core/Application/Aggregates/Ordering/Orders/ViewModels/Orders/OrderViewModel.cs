using Application.Aggregates.Customers;
using Application.Aggregates.Ordering.Orders.ViewModels.OrdersItems;
using Domain.Aggregates.Ordering.Baskets.Enums;
using Domain.Aggregates.Ordering.Orders;
using Mapster;

namespace Application.Aggregates.Ordering.Orders.ViewModels.Orders;

public class OrderViewModel
{
    public OrderViewModel()
    {

    }


    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Guid OwnerId { get; set; }
    public string BasketReferenceNumber { get; set; }
    public string ReferenceNumber { get; set; }
    // Order Status Property !!!
    public Platform Platform { get; set; }
    public string? Description { get; set; }
    public decimal TotalDiscountAmount { get; set; }
    public DiscountType TotalDiscountType { get; set; }
    public decimal TotalWithoutDiscount { get; set; }
    public decimal SubtotalBeforeBasketDiscount { get; set; }
    public decimal OrderTotal { get; set; }
    public List<OrderItemViewModel> OrderItems { get; set; }
    public decimal TotalItemDiscounts { get; set; }
    public long InsertDateTime { get; set; }

    public string InsertDateTimeFa
    {
        get
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(InsertDateTime)
                .ToString("yyyy/MM/dd HH:ss", new System.Globalization.CultureInfo("fa-IR"));
        }
    }

    public CustomerViewModel Customer { get; set; }


    internal static OrderViewModel FromOrder(Order order)
    {
        return new OrderViewModel
        {
            Id = order.Id,
            CustomerId = order.CustomerId,
            OwnerId = order.OwnerId,
            BasketReferenceNumber = order.BasketReferenceNumber,
            ReferenceNumber = order.ReferenceNumber,
            TotalDiscountType = order.TotalDiscountAmount.DiscountType,
            TotalDiscountAmount = order.TotalDiscountAmount.Value,
            Description = order.Description,
            Platform = order.Platform,
            OrderTotal = order.Total,
            TotalWithoutDiscount = order.TotalWithoutDiscount,
            SubtotalBeforeBasketDiscount = order.TotalBeforeDiscount,
            TotalItemDiscounts = order.TotalItemDiscounts,
            InsertDateTime = order.InsertDateTime,
            Customer = order.Customer.Adapt<CustomerViewModel>(),
            OrderItems = order.OrderItems.Select(x => new OrderItemViewModel
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
            }).ToList()
        };
    }

    internal static List<OrderViewModel> FromOrderList(List<Order> orders)
    {
        return orders.Select(x => new OrderViewModel()
        {
            Id = x.Id,
            CustomerId = x.CustomerId,
            OwnerId = x.OwnerId,
            BasketReferenceNumber = x.BasketReferenceNumber,
            ReferenceNumber = x.ReferenceNumber,
            TotalDiscountType = x.TotalDiscountAmount.DiscountType,
            TotalDiscountAmount = x.TotalDiscountAmount.Value,
            Description = x.Description,
            Platform = x.Platform,
            OrderTotal = x.Total,
            TotalWithoutDiscount = x.TotalWithoutDiscount,
            SubtotalBeforeBasketDiscount = x.TotalBeforeDiscount,
            TotalItemDiscounts = x.TotalItemDiscounts,
            InsertDateTime = x.InsertDateTime,
            Customer = x.Customer.Adapt<CustomerViewModel>(),
            OrderItems = x.OrderItems.Select(orderItem => new OrderItemViewModel
            {
                Id = orderItem.Id,
                BasePrice = orderItem.ProductAmount.BasePrice,
                DiscountType = orderItem.DiscountAmount.DiscountType,
                DiscountValue = orderItem.DiscountAmount.Value,
                ProductId = orderItem.Product.ProductId,
                ProductName = orderItem.Product.ProductName,
                Quantity = orderItem.ProductAmount.Quantity,
                TotalPrice = orderItem.TotalPrice,
                TotalPriceWithAdjustment = orderItem.TotalPriceWithAdjustment,
                PriceAdjustments = orderItem.PriceAdjustments,
                Attributes = OrderItemAttributeContract.FromBasketItemAttribute(orderItem.OrderItemAttribute),
            }).ToList()
        }).ToList();
    }
}
