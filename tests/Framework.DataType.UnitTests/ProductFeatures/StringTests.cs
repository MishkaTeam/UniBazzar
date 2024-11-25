using FluentAssertions;

namespace Framework.DataType.UnitTests.ProductFeatures;

public class StringTests
{
	[Fact]
	public void Create_WithSpaceInKayAndValue_ShouldFixSpaces()
	{
		Guid productId = Guid.NewGuid();
		string key = " Key    Space";
		string value = " Value    Space ";

		key = key!.Fix();
		value = value!.Fix();

		string fixedKey = "Key Space";
		string fixedValue = "Value Space";

		key.Should().Be(fixedKey);
		value.Should().Be(fixedValue);
	}

	[Fact]
	public void Create_WithWhiteSpaceKayAndValue_ShouldReturnNull()
	{
		Guid productId = Guid.NewGuid();
		string key = "    ";
		string value = "    ";

		key = key!.Fix();
		value = value!.Fix();

		key.Should().Be(null);
		value.Should().Be(null);
	}
}