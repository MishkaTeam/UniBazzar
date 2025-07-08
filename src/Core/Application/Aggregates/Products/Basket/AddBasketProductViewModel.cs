namespace Application.Aggregates.Products.Basket;

public class AddBasketProductViewModel
{
    public Guid BasketId { get; set; }
    public Guid ProductId { get; set; }
    public Dictionary<string, Guid> SelectedAttributes { get; set; } = [];
}
