﻿using Entity = BuildingBlocks.Domain.Aggregates.Entity;

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

    public void Update(string title)
    {
        Title = title;
    }

    public bool Exists(Guid productId)
    {
        return Items.Any(x => x.ProductId == productId);
    }

    public void RemoveItem(Guid priceListItemId)
    {
        var item = Items.FirstOrDefault(x => x.Id == priceListItemId);
        if (item != null)
            Items.Remove(item);
    }
}
