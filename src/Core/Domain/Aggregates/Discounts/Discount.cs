using BuildingBlocks.Domain.Aggregates;
using BuildingBlocks.Domain.SeedWork;
using Domain.Aggregates.Discounts.DsiscounProducts;
using Framework.DataType;

namespace Domain.Aggregates.Discounts;

public class Discount : Entity, IEntityHasIsActive
{

	public Discount()
	{
		//For EF
	}

	public static Discount Create(string title, string discountCode, bool isActive, string type,
					 int minimum, int maximum, DateTime start, DateTime end, int amount)
	{
		var discount = new Discount(title, discountCode, isActive, type,
					 minimum, maximum, start, end, amount)
		{
			End = end,
			Type = type,
			Start = start,
			Amount = amount,
			Maximum = maximum,
			Minimum = minimum,
			IsActive = isActive,
			Title = title.Fix(),
			DiscountCode = discountCode,
		};

		return discount;
	}

	public void Update(string title, string discountCode, bool isActive, string type,
					 int minimum, int maximum, DateTime start, DateTime end, int amount)
	{
		End = end;
		Type = type;
		Start = start;
		Amount = amount;
		Maximum = maximum;
		Minimum = minimum;
		IsActive = isActive;
		Title = title.Fix();
		DiscountCode = discountCode;

		SetUpdateDateTime();
	}

	public void Activate()
	{
		IsActive = true;
		RemoveDeactivateTime();
	}

	public void Deactivate()
	{
		IsActive = false;
		SetDeactivateTime();
	}

	public void SetDeactivateTime()
	{
		DeactivateDate = DateTime.Now;
	}

	public void RemoveDeactivateTime()
	{
		DeactivateDate = null;
	}

	public string Title { get; set; }
	public string DiscountCode { get; set; }
	public bool IsActive { get; set; }
	public int Minimum { get; set; }
	public int Maximum { get; set; }
	public int Amount { get; set; }
	public string Type { get; set; }
	public DateTime Start { get; set; }
	public DateTime End { get; set; }
	public DateTime? DeactivateDate { get; set; }
	public List<DiscountProduct> DiscountProducts { get; set; }

	private Discount(string title, string discountCode, bool isActive, string type,
					 int minimum, int maximum, DateTime start, DateTime end, int amount)
	{
		End = end;
		Type = type;
		Start = start;
		Amount = amount;
		Maximum = maximum;
		Minimum = minimum;
		IsActive = isActive;
		Title = title.Fix();
		DiscountCode = discountCode;
	}
}
