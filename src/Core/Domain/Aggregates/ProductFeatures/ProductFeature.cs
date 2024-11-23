using BuildingBlocks.Domain.Aggregates;
using Resources;
using Resources.Messages;

namespace Domain.Aggregates.ProductFeature;

public class ProductFeature : Entity
{
	public ProductFeature()
	{
		// FOR EF!
	}


	public static ProductFeature Create(Guid productId, string key, string value, bool isPinned = false, int order = 100)
	{
		var productFeature = new ProductFeature(productId, key, value, isPinned, order)
		{
			ProductId = ValidateProduct(productId),
			Key = FixString(key),
			Value = FixString(value),
		};

		return productFeature;
	}

	public void Update(string key, string value, bool isPinned = false, int order = 100)
	{
		Key = FixString(key);
		Value = FixString(value);
		IsPinned = isPinned;
		Order = order;
	}

	public Guid ProductId { get; protected set; }
	//public Product Product { get; protected set; }
	public string Key { get; protected set; }
	public string Value { get; protected set; }
	public bool IsPinned { get; protected set; }
	public int Order { get; protected set; }

	private ProductFeature(Guid productId, string key, string value, bool isPinned, int order)
	{
		ProductId = productId;
		Key = key;
		Value = value;
		IsPinned = isPinned;
		Order = order;
	}

	private static Guid ValidateProduct(Guid productId)
	{
		if (productId == Guid.Empty)
		{
			var message = string.Format(Validations.Required, DataDictionary.ProductId);

			throw new ArgumentException(message);
		}

		return productId;
	}

	private static string FixString(string value)
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			return string.Empty;
		}

		value = value.Trim();

		while (value.Contains(value: "  "))
		{
			value = value.Replace("  ", " ");
		}

		return value;
	}
}