using Domain.BuildingBlocks.Aggregates;

namespace Domain.Aggregates.Ordering.Baskets;

public class BasketItem : Entity
{
    public Guid BasketId { get; private set; }
    public long BasketReferenceNumber { get; private set; }
    public long Quantity { get; private set; }

    private BasketItem(Guid basketId, long basketReferenceNumber, long quantity)
    {
        BasketId = basketId;
        BasketReferenceNumber = basketReferenceNumber;
        Quantity = quantity;
    }

    public static BasketItem Create(Guid basketItemBasketId, long basketReferenceNumber, Guid productId, long quantity)
    {
        var basketItem = new BasketItem(basketItemBasketId, basketReferenceNumber, quantity);
        return basketItem;
    }
}