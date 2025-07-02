namespace Domain.Aggregates.Ordering.ValueObjects;

public class ProductAmount : IEquatable<ProductAmount>
{
    public long Quantity { get; private set; }
    public decimal BasePrice { get; private set; }
    public decimal TotalPrice => Quantity * BasePrice;


    protected ProductAmount()
    {
    }

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

    public void PlusQuantity()
    {
        Quantity++;
    }

    public void MinusQuantity()
    {
        if (Quantity <= 1)
        {
            return;
        }

        Quantity--;
    }

    public void SetQuantity(long quantity)
    {
        if (quantity <= 0)
        {
            return;
        }

        Quantity = quantity;
    }

    public void SetAffectedQuantity(long affectedQuantity)
    {
        if (Quantity + affectedQuantity <= 0)
        {
            Quantity = 1;

            return;
        }

        Quantity += affectedQuantity;
    }

    public void SetBasePrice(decimal basePrice)
    {
        if (basePrice < 0)
        {
            return;
        }

        BasePrice = basePrice;
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
