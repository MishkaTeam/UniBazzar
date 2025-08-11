using System.ComponentModel.DataAnnotations;
using Resources;

namespace Application.Aggregates.HomeViews.ViewModels.ProductViewItems;

public class ProductViewItemViewModel : UpdateProductViewItemViewModel
{
    public ProductViewItemViewModel()
    {
    }


    [Display
        (ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Product))]
    public string? ProductName { get; set; }

}
