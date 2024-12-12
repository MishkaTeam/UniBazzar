using BuildingBlocks.Domain.Aggregates;
using Framework.DataType;
using Resources;
using Resources.Messages;

namespace Domain.Aggregates.Products.ProductFeatures;

public class ProductFeature : Entity
{
    public ProductFeature()
    {
        // FOR EF!
    }


    public static ProductFeature Create(Guid productId, string key, string value, bool isPinned = false, int order = 100)
    {
        var productFeature = new ProductFeature(productId, key, value, isPinned, order)
        {
            ProductId = ValidateProduct(productId),
            Key = key.Fix(),
            Value = value.Fix(),
        };

        return productFeature;
    }

    public void Update(string key, string value, bool isPinned = false, int order = 100)
    {
        Key = key.Fix()!;
        Value = value.Fix()!;
        IsPinned = isPinned;
        Order = order;
    }

    public Guid ProductId { get; protected set; }
    //public Product Product { get; protected set; }
    public string? Key { get; protected set; }
    public string? Value { get; protected set; }
    public bool IsPinned { get; protected set; }
    public int Order { get; protected set; }

    private ProductFeature(Guid productId, string key, string value, bool isPinned, int order)
    {
        ProductId = productId;
        Key = key;
        Value = value;
        IsPinned = isPinned;
        Order = order;
    }

    private static Guid ValidateProduct(Guid productId)
    {
        if (productId == Guid.Empty)
        {
            var message = string.Format(Validations.Required, DataDictionary.ProductId);

            throw new ArgumentException(message);
        }

        return productId;
    }

}