using Domain.Aggregates.Ordering.ValueObjects;

namespace Domain.UnitTests.Aggregates.Ordering.BasketTests;
using FluentAssertions;
using Xunit;

public class ProductTypeTests
{
    [Fact]
    public void Create_ValidValues_CreatesProductType()
    {
        var productId = Guid.NewGuid();
        string productName = "Test Product";

        var productType = ProductType.Create(productId, productName);

        productType.ProductId.Should().Be(productId);
        productType.ProductName.Should().Be(productName);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Create_EmptyOrNullProductName_ThrowsArgumentException(string invalidName)
    {
        var productId = Guid.NewGuid();

        Action act = () => ProductType.Create(productId, invalidName);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Create_EmptyGuid_ThrowsArgumentException()
    {
        var emptyGuid = Guid.Empty;
        string productName = "Test Product";

        Action act = () => ProductType.Create(emptyGuid, productName);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Properties_AreReadOnly()
    {
        var productId = Guid.NewGuid();
        string productName = "Test Product";
        var productType = ProductType.Create(productId, productName);

        var productIdProperty = typeof(ProductType).GetProperty(nameof(ProductType.ProductId));
        var productNameProperty = typeof(ProductType).GetProperty(nameof(ProductType.ProductName));

        productIdProperty!.GetSetMethod(true).Should().BeNull("ProductId should be read-only");
        productNameProperty!.GetSetMethod(true).Should().BeNull("ProductName should be read-only");
    }
}