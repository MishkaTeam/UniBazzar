using Domain.Aggregates.ProductImages;
using FluentAssertions;

namespace Domain.UnitTests.Aggregates.ProductImages;

public class ProductImageTests
{

    [Fact]
    public void Create_WithProductId_ShouldSetProperties()
    {
        Guid productid = Guid.NewGuid();
        string imageurl = "test.png";

        var productimage = ProductImage.Create(productid, imageurl);

        productimage.ProductId.Should().Be(productid);
        productimage.ImageUrl.Should().Be(imageurl);
    }

    [Fact]
    public void Create_WithoutProductId_ShouldThrowArgumentException()
    {
        Guid productid = Guid.Empty;
        string imageurl = "test.png";

        Action action = () => ProductImage.Create(productid,imageurl);

        var message = string.Format(Resources.Messages.Validations.Required, Resources.DataDictionary.ProductId);

        action.Should().Throw<ArgumentException>().WithMessage(message);
    }

}
