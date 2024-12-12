using BuildingBlocks.Domain.Aggregates;
using Domain.Aggregates.ProductPriceLists;
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
		Guid storeId, Guid categoryId, Guid brandId, Guid unitId, Guid activePriceListId,
		ProductType productType = ProductType.Product, string? downloadUrl = null)
	{
		var product = new Product(
			name, shortDescription, fullDescription, storeId,
			categoryId, brandId, unitId, activePriceListId,
			productType, downloadUrl)
		{
			Name = name.Fix() ?? "",
			ShortDescription = shortDescription.Fix() ?? "",
			FullDescription = fullDescription.Fix() ?? "",
		};

		ValidateRelations(storeId, categoryId, brandId, unitId, activePriceListId);

		return product;
	}

	public void Update(string name, string shortDescription, string fullDescription,
		Guid storeId, Guid categoryId, Guid brandId, Guid unitId, Guid activePriceListId,
		ProductType productType = ProductType.Product, string? downloadUrl = null)
	{
		Name = name.Fix() ?? "";
		ShortDescription = shortDescription.Fix() ?? "";
		FullDescription = fullDescription.Fix() ?? "";

		ValidateRelations(storeId, categoryId, brandId, unitId, activePriceListId);

		StoreId = storeId;
		CategoryId = categoryId;
		BrandId = brandId;
		UnitId = unitId;
		ActivePriceListId = activePriceListId;

		ProductType = productType;
		DownloadUrl = downloadUrl;
	}

	public string Name { get; protected set; }
	public string ShortDescription { get; protected set; }
	public string FullDescription { get; protected set; }
	public string SKU { get; }
	public string? DownloadUrl { get; protected set; }

	public ProductType ProductType { get; protected set; }

	public Guid ActivePriceListId { get; protected set; }
	//public ProductPriceList ActivePriceList { get; protected set; }

	public Guid UnitId { get; protected set; }
	public Unit Unit { get; protected set; }

	public Guid BrandId { get; protected set; }
	//public Brand Brand { get; protected set; }

	public Guid CategoryId { get; protected set; }
	//public Category Category { get; protected set; }

	public Guid StoreId { get; protected set; }
	//public Store Store { get; protected set; }

	private Product(string name, string shortDescription, string fullDescription,
		Guid storeId, Guid categoryId, Guid brandId, Guid unitId, Guid activePriceListId,
		ProductType productType = ProductType.Product, string? downloadUrl = null)
	{
		Name = name;
		ShortDescription = shortDescription;
		FullDescription = fullDescription;
		StoreId = storeId;
		CategoryId = categoryId;
		BrandId = brandId;
		UnitId = unitId;
		ActivePriceListId = activePriceListId;
		ProductType = productType;
		DownloadUrl = downloadUrl;

		SKU = GenerateSKU();
	}

	private string GenerateSKU()
	{
		var constWord = "UBZ";

		var unixTime = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();

		var SKU = $"{constWord}-{constWord}";

		return SKU;
	}

	private static void ValidateRelations(
		Guid storeId, Guid categoryId, Guid brandId,
		Guid unitId, Guid activePriceListId)
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
		else if (activePriceListId == Guid.Empty)
		{
			message = string.Format(
				Validations.Required, DataDictionary.ActivePriceListId);
		}
		else
		{
			return;
		}

		throw new ArgumentException(message);
	}

}