using Domain.Aggregates.Ordering.Baskets.Enums;
using Entity = Domain.BuildingBlocks.Aggregates.Entity;

namespace Domain.Aggregates.Ordering.Baskets;

public class Basket : Entity
{
    public string ReferenceNumber { get; private set; }
    public BasketStatus BasketStatus { get; private set; }
    public Platform PlatForm { get; private set; }
    public List<BasketItem> BasketItems  { get; private set; }

    private Basket()
    {
        // FOR EF!
    }

    private Basket(Platform platForm)
    {
        PlatForm = platForm;
        BasketItems = new List<BasketItem>();
        BasketStatus = BasketStatus.INITIAL;

        // For test
        ReferenceNumber = Guid.NewGuid().ToString();
    }

    public static Basket Initialize(Platform platForm)
    {
        var basket = new Basket(platForm);
        return basket;
    }

    public void AddItem(BasketItem basketItem)
    {
        BasketItems.Add(basketItem);
    }


    public void Checkout()
    {
        BasketStatus = BasketStatus.CHECKOUT;
    }
}