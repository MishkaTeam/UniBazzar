using System.ComponentModel.DataAnnotations;
using Domain.Aggregates.Cms.HomeViews.Enums;
using Resources;

namespace Application.Aggregates.HomeViews.ViewModels;

public class HomeViewViewModel : UpdateHomeViewViewModel
{
    public HomeViewViewModel()
    {
    }


    public bool IsActive { get; set; }
    public bool IsSystemic { get; set; }

    [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Type))]
    public string TypeName
    {
        get
        {
            return Type switch
            {
                ViewType.Slider => DataDictionary.Slider,
                ViewType.Product => DataDictionary.ProductView,
                ViewType.Image => DataDictionary.ImageView,
                _ => throw new InvalidOperationException("Unknown view type.")
            };
        }
    }
}
