namespace Domain.Aggregates.Ordering.Baskets.Enums;

public enum BasketStatus : byte
{
    INITIAL = 0,
    CLOSED = 1,
    CANCELLED = 2,
}