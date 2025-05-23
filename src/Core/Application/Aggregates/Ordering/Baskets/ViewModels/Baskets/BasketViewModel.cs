using Application.Aggregates.Orders.ViewModels.BasketItems;
using Domain.Aggregates.Ordering.Baskets;
using Domain.Aggregates.Ordering.Baskets.Enums;

namespace Application.Aggregates.Ordering.Baskets.ViewModels.Baskets;

public class BasketViewModel
{
    public string ReferenceNumber { get; set; }
    public BasketStatus BasketStatus { get; set; }
    public Platform PlatForm { get; set; }
    public List<BasketItemViewModel> BasketItems { get; private set; }

}