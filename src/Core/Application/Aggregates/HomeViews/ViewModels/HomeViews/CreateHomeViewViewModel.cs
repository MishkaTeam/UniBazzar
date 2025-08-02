using System.ComponentModel.DataAnnotations;
using Domain.Aggregates.Cms.HomeViews.Enums;

namespace Application.Aggregates.HomeViews.ViewModels.HomeViews;

public class CreateHomeViewViewModel
{
    public CreateHomeViewViewModel()
    {
    }


    [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Title))]
    public string Title { get; set; }

    [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Type))]
    public ViewType Type { get; set; }

    [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Order))]
    public int Ordering { get; set; }

}
