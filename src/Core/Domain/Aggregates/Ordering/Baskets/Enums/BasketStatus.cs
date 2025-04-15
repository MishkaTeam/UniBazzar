namespace Domain.Aggregates.Ordering.Baskets.Enums;

public enum BasketStatus : byte
{
    INITIAL = 0,
    CHECKOUT = 1,
    CLOSED = 2,
    CANCELLED = 3,
}