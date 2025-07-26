using System.ComponentModel.DataAnnotations;
using Domain.Aggregates.Cms.HomeViews.Enums;
using Resources;

namespace Application.Aggregates.HomeViews.ViewModels.HomeViews;

public class HomeViewViewModel : UpdateHomeViewViewModel
{
    public HomeViewViewModel()
    {
    }


    public bool IsActive { get; set; }
    public bool IsSystemic { get; set; }

    [Display
        (ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Type))]
    public string TypeName
    {
        get
        {
            return Type switch
            {
                ViewType.Slider => DataDictionary.SliderView,
                ViewType.Product => DataDictionary.ProductView,
                ViewType.Image => DataDictionary.ImageView,
                _ => throw new InvalidOperationException("Unknown view type.")
            };
        }
    }
}
