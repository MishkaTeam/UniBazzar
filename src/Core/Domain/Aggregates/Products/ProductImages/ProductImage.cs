using Entity = Domain.BuildingBlocks.Aggregates.Entity;

namespace Domain.Aggregates.Products.ProductImages;

public class ProductImage : Entity
{
    public ProductImage()
    {
        // FOR EF!
    }

    public static ProductImage Create(Guid productid, string imageurl)
    {
        var productimage = new ProductImage(productid, imageurl)
        {
            ProductId = ValidateProduct(productid),
            ImageUrl = imageurl
        };

        return productimage;
    }

    public void Update(Guid productid, string imageurl)
    {
        ImageUrl = imageurl;
        ProductId = ValidateProduct(productid);

		SetUpdateDateTime();
	}

	private static Guid ValidateProduct(Guid productid)
    {
        if (productid == Guid.Empty)
        {
            var message = string.Format(Resources.Messages.Validations.Required, Resources.DataDictionary.ProductId);

            throw new ArgumentException(message: message);
        }

        return productid;

    }

    public Guid ProductId { get; private set; }
    public string ImageUrl { get; private set; }

    private ProductImage(Guid productid, string imageurl)
    {
        ImageUrl = imageurl;
        ProductId = productid;
    }

}
