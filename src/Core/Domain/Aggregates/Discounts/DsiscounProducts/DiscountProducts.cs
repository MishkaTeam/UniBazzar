using BuildingBlocks.Domain.Aggregates;
using Domain.Aggregates.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.Discounts.DsiscounProducts;

public class DiscountProduct : Entity
{

	public DiscountProduct()
	{
		//For EF
	}

	public Guid DiscountId { get; set; }
	public Discount Discount { get; set; }
	public Guid ProductId { get; set; }
	public Product Product { get; set; }

	public static DiscountProduct Create(Guid discountId, Guid productId)
	{
		var discountProduct = new DiscountProduct(discountId, productId)
		{
			DiscountId = discountId,
			ProductId = productId,
		};

		return discountProduct;
	}

	public void Update(Guid discountId, Guid productId)
	{
		DiscountId = discountId;
		ProductId = productId;

		SetUpdateDateTime();
	}

	private DiscountProduct(Guid discountId, Guid productId)
	{
		DiscountId = discountId;
		ProductId = productId;
	}

}
