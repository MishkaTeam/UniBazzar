using Application.Aggregates.Ordering.Baskets.ViewModels.BasketItems;

namespace Application.Aggregates.Products.Basket;

public class AddBasketProductViewModel
{
    public Guid? BasketId { get; set; }
    public Guid ProductId { get; set; }
    public Dictionary<Guid, Guid> SelectedAttributes { get; set; } = [];
}
