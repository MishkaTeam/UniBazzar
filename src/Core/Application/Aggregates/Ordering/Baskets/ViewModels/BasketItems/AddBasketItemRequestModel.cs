namespace Application.Aggregates.Orders.ViewModels.BasketItems;

public class AddBasketItemRequestModel
{
    public Guid BasketId { get; private set; }
    public long BasketReferenceNumber { get; private set; }
    public long Quantity { get; private set; }
    public Guid Product { get; set; }
}