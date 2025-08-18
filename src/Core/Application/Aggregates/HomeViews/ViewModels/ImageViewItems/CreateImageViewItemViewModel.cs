using System.ComponentModel.DataAnnotations;
using Resources;

namespace Application.Aggregates.HomeViews.ViewModels.ImageViewItems;

public class CreateImageViewItemViewModel
{
    public CreateImageViewItemViewModel()
    {
        Column = "2";
    }


    public Guid HomeViewId { get; set; }

    [Display
        (ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Title))]
    public string Title { get; set; }

    public string? ImageUrl { get; set; }

    [Display
        (ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.NavigationUrl))]
    public string? NavigationUrl { get; set; }

    [Display
        (ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Column))]
    [Range
        (minimum: 1, 2,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.Range))]
    public string Column { get; set; }

    [Display
        (ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Order))]
    [Range
        (minimum: 0, 100_000,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.Range))]
    public int Ordering { get; set; }

}
