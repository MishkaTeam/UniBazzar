namespace Domain.Aggregates.Ordering.ValueObjects;

public class ProductType 
{
    public string ProductName { get; private set; }
    public Guid ProductId { get; private set; }


    protected ProductType()
    {    
    }

    private ProductType(Guid productId, string productName)
    {
        if (string.IsNullOrWhiteSpace(productName))
            throw new ArgumentException("Product name cannot be empty.", nameof(productName));

        if (productId.Equals(Guid.Empty))
            throw new ArgumentException("Product id cannot be empty.", nameof(productId));

        ProductName = productName;
        ProductId = productId;
    }

    public static ProductType Create(Guid productId, string productName)
    {
        return new ProductType(productId, productName);
    }
}
