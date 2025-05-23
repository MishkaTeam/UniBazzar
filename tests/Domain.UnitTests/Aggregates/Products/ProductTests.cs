﻿using Domain.Aggregates.Products;
using Domain.Aggregates.Products.Enums;
using FluentAssertions;

namespace Domain.UnitTests.Aggregates.Products;

public class ProductTests
{
	[Fact]
	public void Create_WithValidProperties_ShouldSetProperties()
	{
		string name = "Product Name";
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

		product.Name.Should().Be(name);
		product.ShortDescription.Should().Be(shortDescription);
		product.FullDescription.Should().Be(fullDescription);
		product.StoreId.Should().Be(storeId);
		product.CategoryId.Should().Be(categoryId);
		product.UnitId.Should().Be(unitId);
	}
}