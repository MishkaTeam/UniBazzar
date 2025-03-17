namespace Domain.Aggregates.Ordering.ValueObjects;

public class ProductAmount : IEquatable<ProductAmount>
{
    public long Quantity { get; }
    public decimal BasePrice { get; }
    public decimal TotalPrice => Quantity * BasePrice;

    private ProductAmount(long quantity, decimal basePrice)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

        if (basePrice < 0)
            throw new ArgumentException("Base price cannot be negative.", nameof(basePrice));

        Quantity = quantity;
        BasePrice = basePrice;
    }

    public static ProductAmount Create(long quantity, decimal basePrice)
    {
        return new ProductAmount(quantity, basePrice);
    }

    public override bool Equals(object? obj) => obj is ProductAmount other && Equals(other);

    public bool Equals(ProductAmount? other)
    {
        return other is not null &&
               Quantity == other.Quantity &&
               BasePrice == other.BasePrice;
    }

    public override int GetHashCode() => HashCode.Combine(Quantity, BasePrice);

}
