using Domain.Aggregates.Customers;
using Domain.Aggregates.Products;
using Domain.BuildingBlocks.Aggregates;
using Framework.DataType;

namespace Domain.Aggregates.Comments;

public class Comment : Entity
{
    public Comment()
    {
    }


    public string Text { get; set; }

    public Guid CutomerId { get; set; }
    public Customer Customer { get; set; }

    public Guid ProductId { get; set; }
    public Product Product { get; set; }

    public static Comment Create(string text, Guid customerId, Guid productId)
    {
        var comment = new Comment(text, customerId, productId)
        {
            Text = text.Fix()
        };
        return comment;
    }

    public void Update(string text)
    {
        var comment = new Comment()
        {
            Text = text
        };
    }

    private Comment(string text, Guid customerId, Guid productId)
    {
        Text = text;
    }
}
