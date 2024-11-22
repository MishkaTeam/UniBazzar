using BuildingBlocks.Domain.Aggregates;
using Domain.Aggregates.ProductImages;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Aggregates.ProductPriceLists;

public class ProductPriceList : Entity
{

    public ProductPriceList()
    {
        
    }

    public static ProductPriceList Create(Guid productid, int price)
    {
        var productimage = new ProductPriceList(productid, price)
        {
            ProductId = ValidateProduct(productid),
            Price = ValidatePrice(price),
        };

        return productimage;
    }

    public void Update(Guid productid, int price)
    {
        Price = ValidatePrice(price);
        ProductId = ValidateProduct(productid);
    }

    private static int ValidatePrice(int price)
    {
        if (price < 0)
        {
            var message = string.Format(Resources.Messages.Validations.IP, Resources.DataDictionary.Price);

            throw new ArgumentException(message: message);
        }

        return price;
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

    public Guid ProductId { get; set; }
    public int Price { get; private set; }

    private ProductPriceList(Guid productid, int price)
    {
        Price = price;
        ProductId = productid;
    }

}
