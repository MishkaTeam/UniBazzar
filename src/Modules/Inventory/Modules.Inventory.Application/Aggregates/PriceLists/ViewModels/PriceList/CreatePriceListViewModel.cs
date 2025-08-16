using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.PriceLists.ViewModels.PriceList;

public class CreatePriceListViewModel
{

    [Display
    (ResourceType = typeof(Resources.DataDictionary),
    Name = nameof(Resources.DataDictionary.Name))]
    public string Title { get; set; }
}
