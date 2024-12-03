using Domain.Aggregates.ProductPriceLists;
using FluentAssertions;

namespace Domain.UnitTests.Aggregates.ProductPriceLists;

public class ProductPriceListTests
{

    [Fact]
    public void Create_WithProductIdAndValidPrice_ShouldSetProperties()
    {
        Guid productid = Guid.NewGuid();
        int price = 10000;

        var productpricelists = ProductPriceList.Create(productid, price);

        productpricelists.Price.Should().Be(price);
        productpricelists.ProductId.Should().Be(productid);
    }

    [Fact]
    public void Create_WithoutProductIdAndValidPrice_ShouldThrowArgumentException()
    {
        Guid productid = Guid.Empty;
        int price = 10000;

        Action action = () => ProductPriceList.Create(productid, price);

        var message = string.Format(Resources.Messages.Validations.Required, Resources.DataDictionary.ProductId);

        action.Should().Throw<ArgumentException>().WithMessage(message);
    }

    [Fact]
    public void Create_WithProductIdAndNegativePrice_ShouldThrowArgumentException()
    {
        Guid productid = Guid.NewGuid();
        int price = -10000;

        Action action = () => ProductPriceList.Create(productid, price);

        var message = string.Format(Resources.Messages.Validations.IP, Resources.DataDictionary.Price);

        action.Should().Throw<ArgumentException>().WithMessage(message);
    }

}
