using Framework.DataType;
using Resources;
using Resources.Messages;
using Entity = BuildingBlocks.Domain.Aggregates.Entity;

namespace Domain.Aggregates.Products.ProductFeatures;

public class ProductFeature : Entity
{
	public ProductFeature()
	{
		// FOR EF!
	}
		

	public static ProductFeature Create
		(Guid productId, string key, string value,
		bool isPinned = false, int order = 100)
	{
		var productFeature = new ProductFeature(productId, key, value, isPinned, order)
		{
			ProductId = ValidateProduct(productId),
			Key = key.Fix(),
			Value = value.Fix(),
		};

		return productFeature;
	}

	public void Update
		(string key, string value,
		bool isPinned = false, int order = 100)
	{
		Key = key.Fix()!;
		Value = value.Fix()!;
		IsPinned = isPinned;
		Ordering = order;

		SetUpdateDateTime();
	}

	public string Key { get; private set; }
	public string Value { get; private set; }
	public bool IsPinned { get; private set; }

	public Guid ProductId { get; private set; }
	public Product Product { get; private set; }

	//public Store Store { get; private set; }

	private ProductFeature
		(Guid productId, string key, string value,
		bool isPinned, int order)
	{
		ProductId = productId;
		Key = key;
		Value = value;
		IsPinned = isPinned;
		Ordering = order;
	}

	private static Guid ValidateProduct(Guid productId)
	{
		if (productId == Guid.Empty)
		{
			var message =
				string.Format(Validations.Required, DataDictionary.ProductId);

			throw new ArgumentException(message);
		}

		return productId;
	}
}