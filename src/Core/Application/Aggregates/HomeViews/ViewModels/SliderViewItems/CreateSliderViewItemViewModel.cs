using Resources;
using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.HomeViews.ViewModels.SliderViewItems;

public class CreateSliderViewItemViewModel
{
    public CreateSliderViewItemViewModel()
    {
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
        Name = nameof(DataDictionary.Interval))]
    public int Interval { get; set; }

    [Display
        (ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Order))]
    public int Ordering { get; set; }

}
