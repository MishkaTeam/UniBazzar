using Domain.BuildingBlocks.Aggregates;
using Domain.BuildingBlocks.SeedWork;
using Framework.DataType;
using Microsoft.VisualBasic;

namespace Domain.Aggregates.Discounts;

public class Discount : Entity, IEntityHasIsActive
{

    public Discount()
    {
        //For EF
    }

    public static Discount Create(string title, string discountCode, bool isActive)
    {
        var discount = new Discount(title, discountCode, isActive)
        {
            Title = title.Fix(),
            IsActive = isActive,
            DiscountCode = discountCode
        };

        return discount;
    }

    public void Update(string title, string discountCode, bool isActive)
    {
        Title = title.Fix();
        IsActive = isActive;
        DiscountCode = discountCode;

         SetUpdateDateTime();
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
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
    public DateTime? DeactivateDate { get; set; }

    private Discount(string title, string discountCode, bool isActive)
    {
        Title = title;
        IsActive = isActive;
        DiscountCode = discountCode;
    }
}
