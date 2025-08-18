using Resources;
using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.HomeViews.ViewModels.ProductViewItems;

public class CreateProductViewItemViewModel
{
    public CreateProductViewItemViewModel()
    {
    }


    public Guid HomeViewId { get; set; }

    [Display
        (ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Product))]
    public Guid ProductId { get; set; }

    [Display
        (ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Order))]
    [Range
        (minimum: 0, 100_000,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.Range))]
    public int Ordering { get; set; }

}
