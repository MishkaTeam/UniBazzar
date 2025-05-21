using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.Discounts.ViewModels;

public class DiscountViewModel : CreateDiscountViewModel
{
    [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Id))]
    public Guid Id { get; set; }

    [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.StoreId))]
    public Guid StoreId { get; set; }

    [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.OwnerId))]
    public Guid OwnerId { get; set; }

    [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.InsertedBy))]
    public Guid InsertedBy { get; set; }

    [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.UpdatedBy))]
    public Guid UpdatedBy { get; set; }

    [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.InsertDateTime))]
    public long InsertDateTime { get; set; }

    [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.UpdateDateTime))]
    public long UpdateDateTime { get; set; }
}
