using Domain.Aggregates.Products;
using Domain.Aggregates.Products.Enums;
using FluentAssertions;

namespace Framework.DataType.UnitTests.Products;

public class StringTests
{
	[Fact]
	public void Create_WithSpaceInName_ShouldFixSpaces()
	{
		string name = "     Product        Name        ";
		string shortDescription = "Short Description";
		string fullDescription = "Full Description";
		Guid storeId = Guid.NewGuid();
		Guid categoryId = Guid.NewGuid();
		Guid unitId = Guid.NewGuid();
		Guid activePriceListId = Guid.NewGuid();
		ProductType productType = ProductType.Product;
		string? downloadUrl = null;

		var product = Product.Create(
			name, shortDescription, fullDescription,
			storeId, categoryId, unitId,
			productType, downloadUrl);

		var fixedName = "Product Name";

		product.Name.Should().Be(fixedName);
	}

	[Fact]
	public void Create_WithWhiteSpaceFullDescription_ShouldReturnEmptyString()
	{
		string name = "Product Name";
		string shortDescription = "Short Description";
		string fullDescription = "    ";
		Guid storeId = Guid.NewGuid();
		Guid categoryId = Guid.NewGuid();
		Guid unitId = Guid.NewGuid();
		Guid activePriceListId = Guid.NewGuid();
		ProductType productType = ProductType.Product;
		string? downloadUrl = null;

		var product = Product.Create(
			name, shortDescription, fullDescription,
			storeId, categoryId, unitId,
			productType, downloadUrl);

		var fixedFullDescription = "";

		product.FullDescription.Should().Be(fixedFullDescription);
	}
}