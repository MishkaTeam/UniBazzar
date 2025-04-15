using Domain.Aggregates.Ordering.Baskets.Enums;

namespace Application.Aggregates.Orders.ViewModels;

public class InitializeBasketRequestModel
{
    public Platform platform { get; set; }
}