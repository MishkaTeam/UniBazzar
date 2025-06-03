using Domain.Aggregates.Products;
using Entity = BuildingBlocks.Domain.Aggregates.Entity;

namespace Domain.Aggregates.PriceLists;

public class PriceListItem : Entity
{

    protected PriceListItem()
    {
        // FOR EF!
    }


    public Guid ProductId { get; set; }
    public decimal Price { get; private set; }
    public string CurrencyCode { get; set; }

    public Product Product { get; set; }
    private PriceListItem(Guid productid, decimal price, string currencyCode)
    {
        Price = price;
        ProductId = productid;
        CurrencyCode = currencyCode;
    }

    public static PriceListItem Create(Guid productid, decimal price, string currencyCode)
    {
        var validatedProduct = ValidateProduct(productid);
        var validatedPrice = ValidatePrice(price);
        var priceList = new PriceListItem(validatedProduct, validatedPrice, currencyCode);
        return priceList;
    }

    public void Update(Guid productid, decimal price)
    {
        Price = ValidatePrice(price);
        ProductId = ValidateProduct(productid);

		SetUpdateDateTime();
	}

	private static decimal ValidatePrice(decimal price)
    {
        if (price < 0)
        {
            var message = string.Format(Resources.Messages.Validations.NotValid, Resources.DataDictionary.Price);

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



}
