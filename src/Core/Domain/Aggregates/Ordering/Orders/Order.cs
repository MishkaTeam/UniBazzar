using Entity = Domain.BuildingBlocks.Aggregates.Entity;

namespace Domain.Aggregates.Ordering.Orders;

public class Order : Entity
{
    public long ReferenceNumber { get; private set; }
}