using Domain.Aggregates.Ordering.Baskets.Enums;

namespace Domain.Aggregates.Ordering.ValueObjects;

public class DiscountAmount : IEquatable<DiscountAmount>
{
    public decimal Value { get; private set; }
    public DiscountType DiscountType { get; private set; }


    protected DiscountAmount()
    {
    }

    private DiscountAmount(decimal value, DiscountType discountType)
    {
        if (value < 0)
            throw new ArgumentException("Discount value cannot be negative.", nameof(value));

        if (discountType == DiscountType.Percent && value > 100)
            throw new ArgumentException("Percentage discount cannot exceed 100%.", nameof(value));

        Value = value;
        DiscountType = discountType;
    }

    public static DiscountAmount CreatePriceDiscount(decimal amount)
    {
        return new DiscountAmount(amount, DiscountType.Price);
    }

    public static DiscountAmount CreatePercentDiscount(decimal percentage)
    {
        return new DiscountAmount(percentage, DiscountType.Percent);
    }


    public static DiscountAmount CreateNoDiscount()
    {
        return new DiscountAmount(0, DiscountType.None);
    }

    public void SetValue(decimal value)
    {
        if (value < 0)
            throw new ArgumentException("Discount value cannot be negative.", nameof(value));

        if (DiscountType == DiscountType.Percent && value > 100)
            throw new ArgumentException("Percentage discount cannot exceed 100%.", nameof(value));

        Value = value;
    }
    {
        return DiscountType switch
        {
            DiscountType.Price => Math.Max(0, originalPrice - (Value * quantity)),
            DiscountType.Percent => Math.Max(0, originalPrice * (1 - (Value / 100))),
            DiscountType.None => originalPrice,
            _ => throw new InvalidOperationException("Unknown discount type.")
        };
    }

    public override bool Equals(object? obj) => obj is DiscountAmount other && Equals(other);

    public bool Equals(DiscountAmount? other)
    {
        return other is not null && Value == other.Value && DiscountType == other.DiscountType;
    }

    public override int GetHashCode() => HashCode.Combine(Value, DiscountType);

    public override string ToString() => DiscountType == DiscountType.Price
        ? $"{Value:C} Discount"
        : $"{Value}% Discount";

    public static DiscountAmount Create(decimal amount, DiscountType discountType)
    {
        return discountType switch
        {
            DiscountType.Percent => CreatePercentDiscount(amount),
            DiscountType.Price => CreatePriceDiscount(amount),
            DiscountType.None => CreateNoDiscount(),
            _ => throw new ArgumentOutOfRangeException(nameof(discountType), discountType, null)
        };
    }

}