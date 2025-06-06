using Domain.Aggregates.PriceLists;
using FluentAssertions;

namespace Domain.UnitTests.Aggregates.Products.ProductPriceLists;

public class ProductPriceListTests
{

    //[Fact]
    //public void Create_WithProductIdAndValidPrice_ShouldSetProperties()
    //{
    //    Guid productid = Guid.NewGuid();
    //    int price = 10000;

    //    var productpricelists = PriceList.Create(productid, price);

    //    productpricelists.Price.Should().Be(price);
    //    productpricelists.ProductId.Should().Be(productid);
    //}

    //[Fact]
    //public void Create_WithoutProductIdAndValidPrice_ShouldThrowArgumentException()
    //{
    //    Guid productid = Guid.Empty;
    //    int price = 10000;

    //    Action action = () => PriceList.Create(productid, price);

    //    var message = string.Format(Resources.Messages.Validations.Required, Resources.DataDictionary.ProductId);

    //    action.Should().Throw<ArgumentException>().WithMessage(message);
    //}

    //[Fact]
    //public void Create_WithProductIdAndNegativePrice_ShouldThrowArgumentException()
    //{
    //    Guid productid = Guid.NewGuid();
    //    int price = -10000;

    //    Action action = () => PriceList.Create(productid, price);

    //    var message = string.Format(Resources.Messages.Validations.NotValid, Resources.DataDictionary.Price);

    //    action.Should().Throw<ArgumentException>().WithMessage(message);
    //}

}
