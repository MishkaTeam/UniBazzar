using Application.Aggregates.Orders.ViewModels.BasketItems;
using Domain.Aggregates.Ordering.Baskets.Enums;

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
    public List<BasketItemViewModel> BasketItems { get; set; }

}