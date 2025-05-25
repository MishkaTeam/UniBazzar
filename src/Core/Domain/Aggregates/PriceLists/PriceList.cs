using Domain.Aggregates.PriceListItems;
using Entity = Domain.BuildingBlocks.Aggregates.Entity;

namespace Domain.Aggregates.PriceLists;

public class PriceList : Entity
{
    public string Title { get; private set; }

    public List<PriceListItem> Items { get; private set; }

    protected PriceList()
    {
        //FOR EF!
        //
    }

    private PriceList(string title)
    {
        Title = title;
    }

    public static PriceList Create(string title)
    {
        var res = new PriceList(title);
        return res;
    }

    public void AddItem(Guid productId, decimal Price, string CurrencyCode)
    {
        var priceListItem = PriceListItem.Create(productId, Price, CurrencyCode);
        Items.Add(priceListItem);
    }
}
