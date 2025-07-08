using BuildingBlocks.Domain.Aggregates;
using Domain.Aggregates.Customers;
using Domain.Aggregates.Products;
using Framework.DataType;

namespace Domain.Aggregates.ProductReviews;

public class ProductReview : Entity
{
    public ProductReview()
    {
    }

    public string Text { get; set; }

    public byte Rate { get; set; }
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; }

    public Guid ProductId { get; set; }
    public Product Product { get; set; }

    public bool IsVerified { get; set; }

    

    public static ProductReview Create(string text, Guid customerId, Guid productId, byte rate, bool isVerified)
    {
        var comment = new ProductReview(text, customerId, productId, rate, isVerified)
        {
            Text = text.Fix(),
            CustomerId = customerId,
            ProductId = productId,
            Rate = rate,
            IsVerified = isVerified
        };
        return comment;
    }

    public void Update(string text, bool isVerified)
    {
        Text = text;
        IsVerified = isVerified;
    }

    private ProductReview(string text, Guid customerId, Guid productId, byte rate, bool isVerified)
    {
        Text = text;
        CustomerId = customerId;
        ProductId = productId;
        Rate = rate;
        IsVerified = isVerified;
    }
}
