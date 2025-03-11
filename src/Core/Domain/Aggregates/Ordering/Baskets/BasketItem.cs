using System.Security.Cryptography;
using Domain.Aggregates.Ordering.Baskets.ValueObjects;
using Domain.BuildingBlocks.Aggregates;

namespace Domain.Aggregates.Ordering.Baskets;

public class BasketItem : Entity
{
    public Guid BasketId { get; private set; }
    public long BasketReferenceNumber { get; private set; }
    public ProductType Product { get; private set; }
    public ProductAmount ProductAmount { get; private set; }
    public DiscountAmount DiscountAmount { get; private set; }


    private BasketItem(Guid basketId,
        long basketReferenceNumber,
        ProductType product,
        ProductAmount productAmount,
        DiscountAmount discountAmount)
    {
        BasketId = basketId;
        BasketReferenceNumber = basketReferenceNumber;
        Product = product;
        ProductAmount = productAmount;
        DiscountAmount = discountAmount;
    }

    public static BasketItem Create(Guid basketId,
        long basketReferenceNumber,
        ProductType product,
        ProductAmount productAmount,
        DiscountAmount discountAmount)
    {
        var basketItem = new BasketItem(basketId,
            basketReferenceNumber,
            product, 
            productAmount, 
            discountAmount);
        
        return basketItem;
    }
}