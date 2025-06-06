using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.PriceLists.ViewModels.PriceListItem;

public class PriceListItemViewModel : CreatePriceListItemViewModel
{


    [Display
    (ResourceType = typeof(Resources.DataDictionary),
    Name = nameof(Resources.DataDictionary.Name))]
    public string ProductName { get; set; }

    [Display
    (ResourceType = typeof(Resources.DataDictionary),
    Name = nameof(Resources.DataDictionary.Id))]
    public Guid Id { get; set; }

}
