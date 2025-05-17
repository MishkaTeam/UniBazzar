using FluentAssertions;
using Resources.Messages;
using Resources;
using Domain.Aggregates.Products.ProductFeatures;

namespace Domain.UnitTests.Aggregates.Products.ProductFeatures;

public class ProductFeaturesTests
{
    [Fact]
    public void Create_WithValidProperties_ShouldSetProperties()
    {
        Guid productId = Guid.NewGuid();
        string key = "Key";
        string value = "Value";
        bool isPinned = true;
        int order = 100;


        var productFeature =
            ProductFeature.Create(productId, key, value, isPinned, order);


        productFeature.ProductId.Should().Be(productId);
        productFeature.Key.Should().Be(key);
        productFeature.Value.Should().Be(value);
        productFeature.IsPinned.Should().Be(isPinned);
        productFeature.Ordering.Should().Be(order);
    }

    [Fact]
    public void Create_WithEmptyProductId_ShouldThrowArgumentException()
    {
        Guid productId = Guid.Empty;
        string key = "Key";
        string value = "Value";
        bool isPinned = true;
        int order = 100;


        Action action = () => ProductFeature
        .Create(productId, key, value, isPinned, order);


        var message = string.Format(
            Validations.Required, DataDictionary.ProductId);
        action.Should().Throw<ArgumentException>().WithMessage(message);
    }

}