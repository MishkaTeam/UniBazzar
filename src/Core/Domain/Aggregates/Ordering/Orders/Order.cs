using Domain.Aggregates.Ordering.Baskets;
using Domain.Aggregates.Ordering.Baskets.Enums;
using Entity = Domain.BuildingBlocks.Aggregates.Entity;

namespace Domain.Aggregates.Ordering.Orders;

public class Order : Entity
{
    public long ReferenceNumber { get; private set; }
    public Guid BasketId { get; private set; }
    public long BasketReferenceId { get; private set; }
    
    public static Order CreateFromBasket(Basket? basket)
    {
        ArgumentNullException.ThrowIfNull(basket);

        if (basket.BasketStatus != BasketStatus.CHECKOUT)
            throw new Exception("Basket is not in Check out");
        
        throw new NotImplementedException();
    }
}