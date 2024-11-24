using Domain.Aggregates.ProductFeature;
using FluentAssertions;

namespace Framework.DataType.UnitTests.ProductFeatures;

public class ProductFeaturesTests
{
	[Fact]
	public void Create_WithSpaceInKayAndValue_ShouldFixSpaces()
	{
		Guid productId = Guid.NewGuid();
		string key = " Key    Space";
		string value = " Value    Space ";


		var productFeature =
			ProductFeature.Create(productId, key, value);


		string fixedKey = "Key Space";
		string fixedValue = "Value Space";

		productFeature.Key.Should().Be(fixedKey);
		productFeature.Value.Should().Be(fixedValue);
	}

	[Fact]
	public void Create_WithWhiteSpaceKayAndValue_ShouldReturnNull()
	{
		Guid productId = Guid.NewGuid();
		string key = "    ";
		string value = "    ";


		var productFeature =
			ProductFeature.Create(productId, key, value);


		productFeature.Key.Should().Be(null);
		productFeature.Value.Should().Be(null);
	}
}