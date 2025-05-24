using Application.Aggregates.PriceLists.ViewModels.PriceListItem;

namespace Application.Aggregates.PriceLists.ViewModels.PriceList;

public class UpdatePriceListViewModel : CreatePriceListItemViewModel
{

    public Guid Id { get; set; }
}
