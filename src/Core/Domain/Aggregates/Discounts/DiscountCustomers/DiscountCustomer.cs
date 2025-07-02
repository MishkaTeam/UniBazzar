using BuildingBlocks.Domain.Aggregates;
using Domain.Aggregates.Customers;

namespace Domain.Aggregates.Discounts.DiscountCustomers;

public class DiscountCustomer : Entity
{

	public DiscountCustomer()
	{
		// For EF
	}

	public Guid DiscountId { get; set; }
	public Discount Discount { get; set; }
	public Guid CustomerId { get; set; }
	public Customer Customer { get; set; }

	public static DiscountCustomer Create(Guid discountId, Guid customerId)
	{
		var discountProduct = new DiscountCustomer(discountId, customerId)
		{
			DiscountId = discountId,
			CustomerId = customerId,
		};

		return discountProduct;
	}

	public void Update(Guid discountId, Guid customerId)
	{
		DiscountId = discountId;
		CustomerId = customerId;

		SetUpdateDateTime();
	}

	private DiscountCustomer(Guid discountId, Guid customerId)
	{
		DiscountId = discountId;
		CustomerId = customerId;
	}

}
