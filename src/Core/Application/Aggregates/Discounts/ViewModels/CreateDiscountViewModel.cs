using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.Discounts.ViewModels;

public class CreateDiscountViewModel
{

    [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Title))]
    public string Title { get; set; }

    [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.DiscountCode))]
    public string DiscountCode { get; set; }

    [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.IsActive))]
    public bool IsActive { get; set; }

}
