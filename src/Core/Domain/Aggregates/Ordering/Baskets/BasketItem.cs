using System.Security.Cryptography;
using Domain.Aggregates.Ordering.ValueObjects;
using BuildingBlocks.Domain.Aggregates;

namespace Domain.Aggregates.Ordering.Baskets;

public class BasketItem : Entity
{
    public Guid BasketId { get; private set; }
    public string BasketReferenceNumber { get; private set; }
    public ProductType Product { get; private set; }
    public ProductAmount ProductAmount { get; private set; }
    public DiscountAmount DiscountAmount { get; private set; }
    public List<BasketItemAttribute>? BasketItemAttributes { get; private set; }

    public decimal TotalPrice => DiscountAmount.ApplyDiscount(ProductAmount.TotalPrice);
    public decimal DiscountPrice => DiscountAmount.ConvertToPrice(ProductAmount.TotalPrice, ProductAmount.Quantity);
    protected BasketItem()
    {
        //FOR EF!
    }

    private BasketItem(Guid basketId,
        string basketReferenceNumber,
        ProductType product,
        ProductAmount productAmount,
        DiscountAmount discountAmount,
        List<BasketItemAttribute>? basketItemAttributes)
    {
        BasketId = basketId;
        BasketReferenceNumber = basketReferenceNumber;
        Product = product;
        ProductAmount = productAmount;
        DiscountAmount = discountAmount;
        BasketItemAttributes = basketItemAttributes;
    }

    public static BasketItem Create(Guid basketId,
        string basketReferenceNumber,
        ProductType product,
        ProductAmount productAmount,
        DiscountAmount discountAmount,
        List<BasketItemAttribute> basketItemAttributes = null!)
    {
        var basketItem = new BasketItem(basketId,
            basketReferenceNumber,
            product, 
            productAmount, 
            discountAmount,
            basketItemAttributes);
        
        return basketItem;
    }
}