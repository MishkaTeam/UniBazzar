﻿using Domain.Aggregates.Categories;
using Domain.Aggregates.PriceLists;
using Domain.Aggregates.Products.Enums;
using Domain.Aggregates.Products.ProductAttributes;
using Domain.Aggregates.Products.ProductFeatures;
using Domain.Aggregates.Products.ProductImages;
using Domain.Aggregates.Units;
using Framework.DataType;
using Resources;
using Resources.Messages;
using Entity = BuildingBlocks.Domain.Aggregates.Entity;

namespace Domain.Aggregates.Products;

public class Product : Entity
{
	public Product()
	{
		// FOR EF!
	}


	public static Product Create(string name, string shortDescription, string fullDescription,
		Guid storeId, Guid categoryId, Guid unitId,
		ProductType productType = ProductType.Product, string? downloadUrl = null)
	{
		ValidateRelations(storeId, categoryId, unitId);

		downloadUrl = CheckHaveDownloadUrl(productType, downloadUrl);

		var product = new Product(
			name, shortDescription, fullDescription,
			storeId, categoryId, unitId,
			productType, downloadUrl)
		{
			Name = name.Fix() ?? "",
			ShortDescription = shortDescription.Fix() ?? "",
			FullDescription = fullDescription.Fix() ?? "",
		};

		return product;
	}

	public void Update(string name, string shortDescription, string fullDescription,
		Guid storeId, Guid categoryId, Guid unitId,
		ProductType productType = ProductType.Product, string? downloadUrl = null)
	{
		Name = name.Fix() ?? "";
		ShortDescription = shortDescription.Fix() ?? "";
		FullDescription = fullDescription.Fix() ?? "";

		ValidateRelations(storeId, categoryId, unitId);

		StoreId = storeId;
		CategoryId = categoryId;
		UnitId = unitId;

		ProductType = productType;
		DownloadUrl = CheckHaveDownloadUrl(productType, downloadUrl);

		SetUpdateDateTime();
	}

	public string Name { get; private set; }
	public string ShortDescription { get; private set; }
	public string FullDescription { get; private set; }
	public string SKU { get; private set; }
	public string? DownloadUrl { get; private set; }
    public string Slug { get; private set; }

    public ProductType ProductType { get; private set; }

	public Guid UnitId { get; private set; }
	public Unit Unit { get; private set; }

	public Guid CategoryId { get; private set; }

	public Category Category { get; private set; }
	public List<ProductImage> ProductImages { get; private set; }
    public List<ProductFeature> ProductFeatures { get; private set; }
    public List<ProductAttribute> ProductAttributes { get; private set; }

	private Product(string name, string shortDescription, string fullDescription,
		Guid storeId, Guid categoryId, Guid unitId,
		ProductType productType = ProductType.Product, string? downloadUrl = null, string? slug = null)
	{
        slug ??= name.GenerateSlug();

        Name = name;
		ShortDescription = shortDescription;
		FullDescription = fullDescription;
		StoreId = storeId;
		CategoryId = categoryId;
		UnitId = unitId;
		ProductType = productType;
		DownloadUrl = downloadUrl;
		Slug = slug;
		SKU = GenerateSKU();
	}

	private string GenerateSKU()
	{
		var constWord = "UBZ";

		var unixTime = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();

		var SKU = $"{constWord}-{unixTime}";

		return SKU;
	}

	private static void ValidateRelations(
		Guid storeId, Guid categoryId, Guid unitId)
	{
		string? message = null;

		if (storeId == Guid.Empty)
		{
			message = string.Format(
				Validations.Required, DataDictionary.StoreId);
		}
		else if (categoryId == Guid.Empty)
		{
			message = string.Format(
				Validations.Required, DataDictionary.CategoryId);
		}
		else if (unitId == Guid.Empty)
		{
			message = string.Format(
				Validations.Required, DataDictionary.UnitId);
		}
		else
		{
			return;
		}

		throw new ArgumentException(message);
	}

	private static string? CheckHaveDownloadUrl(ProductType productType, string? downloadUrl)
	{
		if (productType == ProductType.Product)
		{
			return null;
		}

		return downloadUrl;
	}
}