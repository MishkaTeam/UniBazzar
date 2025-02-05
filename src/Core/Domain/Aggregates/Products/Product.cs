using BuildingBlocks.Domain.Aggregates;
using Domain.Aggregates.Categories;
using Domain.Aggregates.Units;
using Domain.Enumerations;
using Framework.DataType;
using Resources;
using Resources.Messages;

namespace Domain.Aggregates.Products;

public class Product : Entity
{
	public Product()
	{
		// FOR EF!
	}


	public static Product Create(string name, string shortDescription, string fullDescription,
		Guid storeId, Guid categoryId, Guid brandId, Guid unitId,
		ProductType productType = ProductType.Product, string? downloadUrl = null)
	{
		ValidateRelations(storeId, categoryId, brandId, unitId);

		downloadUrl = CheckHaveDownloadUrl(productType, downloadUrl);

		var product = new Product(
			name, shortDescription, fullDescription, storeId,
			categoryId, brandId, unitId,
			productType, downloadUrl)
		{
			Name = name.Fix() ?? "",
			ShortDescription = shortDescription.Fix() ?? "",
			FullDescription = fullDescription.Fix() ?? "",
		};

		return product;
	}

	public void Update(string name, string shortDescription, string fullDescription,
		Guid storeId, Guid categoryId, Guid brandId, Guid unitId,
		ProductType productType = ProductType.Product, string? downloadUrl = null)
	{
		Name = name.Fix() ?? "";
		ShortDescription = shortDescription.Fix() ?? "";
		FullDescription = fullDescription.Fix() ?? "";

		ValidateRelations(storeId, categoryId, brandId, unitId);

		StoreId = storeId;
		CategoryId = categoryId;
		BrandId = brandId;
		UnitId = unitId;

		ProductType = productType;
		DownloadUrl = CheckHaveDownloadUrl(productType, downloadUrl);
	}

	public string Name { get; private set; }
	public string ShortDescription { get; private set; }
	public string FullDescription { get; private set; }
	public string SKU { get; private set; }
	public string? DownloadUrl { get; private set; }

	public ProductType ProductType { get; private set; }

	public Guid UnitId { get; private set; }
	public Unit Unit { get; private set; }

	public Guid BrandId { get; private set; }
	//public Brand Brand { get; private set; }

	public Guid CategoryId { get; private set; }
	public Category Category { get; private set; }

	//public Guid StoreId { get; private set; }
	//public Store Store { get; private set; }

	private Product(string name, string shortDescription, string fullDescription,
		Guid storeId, Guid categoryId, Guid brandId, Guid unitId,
		ProductType productType = ProductType.Product, string? downloadUrl = null)
	{
		Name = name;
		ShortDescription = shortDescription;
		FullDescription = fullDescription;
		StoreId = storeId;
		CategoryId = categoryId;
		BrandId = brandId;
		UnitId = unitId;
		ProductType = productType;
		DownloadUrl = downloadUrl;

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
		Guid storeId, Guid categoryId,
		Guid brandId, Guid unitId)
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
		else if (brandId == Guid.Empty)
		{
			message = string.Format(
				Validations.Required, DataDictionary.BrandId);
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