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

    public static ProductReview   Create(string text, Guid customerId, Guid productId, byte rate)
    {
        var comment = new ProductReview(text, customerId, productId, rate)
        {
            Text = text.Fix(),
            CustomerId = customerId,
            ProductId = productId,
            Rate = rate,
        };
        return comment;
    }

    public void Update(string text)
    {
        var comment = new ProductReview()
        {
            Text = text
        };
    }

    private ProductReview(string text, Guid customerId, Guid productId, byte rate)
    {
        Text = text;
        CustomerId = customerId;
        ProductId = productId;
        Rate = rate;
    }
}
