using Domain.Aggregates.ProductFeature;
using FluentAssertions;
using Resources.Messages;
using Resources;

namespace Domain.UnitTests.Aggregates.ProductFeatures;

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
		productFeature.Order.Should().Be(order);
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
	public void Create_WithWhiteSpaceKayAndValue_ShouldRemoveSpaces()
	{
		Guid productId = Guid.NewGuid();
		string key = "    ";
		string value = "    ";


		var productFeature =
			ProductFeature.Create(productId, key, value);


		productFeature.Key.Should().Be(string.Empty);
		productFeature.Value.Should().Be(string.Empty);
	}
}