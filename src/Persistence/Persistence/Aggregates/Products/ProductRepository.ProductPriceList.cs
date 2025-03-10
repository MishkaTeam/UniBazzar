﻿using Domain.Aggregates.Products.ProductPriceLists;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Aggregates.Products;

public partial class ProductRepository
{
    public async Task AddProductPriceList(ProductPriceList productPriceList)
    {
        await uniBazzarContext.AddAsync(productPriceList);
    }

    public async Task<ProductPriceList> GetProductPriceListAsync(Guid id)
    {

        var productpricelist = await uniBazzarContext.ProductPriceLists.FirstOrDefaultAsync(x => x.Id == id);
        return productpricelist ?? new ProductPriceList();
    }

    public Task<List<ProductPriceList>> GetAllProductPriceListAsync()
    {

        return uniBazzarContext.ProductPriceLists.ToListAsync();
    }

    public async Task<ProductPriceList> GetPriceByProductId(Guid id)
    {
        var productpricelist = await uniBazzarContext.ProductPriceLists.FirstOrDefaultAsync(x => x.ProductId == id);
        return productpricelist ?? new ProductPriceList();
    }

    public async Task<List<ProductPriceList>> GetPriceListByProductId(Guid id)
    {
        return await uniBazzarContext.ProductPriceLists.Where(x => x.ProductId == id).ToListAsync();
    }

    public void RemovePriceList(ProductPriceList productPriceList)
    {
        uniBazzarContext.ProductPriceLists.Remove(productPriceList);
    }
}
