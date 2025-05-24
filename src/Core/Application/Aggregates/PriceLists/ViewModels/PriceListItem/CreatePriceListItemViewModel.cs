using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.PriceLists.ViewModels.PriceListItem;

public class CreatePriceListItemViewModel
{
    [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.ProductId))]
    public Guid ProductId { get; set; }


    [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Price))]
    public decimal Price { get; set; }

}
