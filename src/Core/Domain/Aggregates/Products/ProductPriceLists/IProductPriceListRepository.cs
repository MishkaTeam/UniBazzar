namespace Domain.Aggregates.Products.ProductPriceLists;

public interface IProductPriceListRepository
{
    void AddProductPriceList(ProductPriceList productPriceList);
    Task<ProductPriceList> GetProductPriceListAsync(Guid id);
    Task<List<ProductPriceList>> GetAllProductPriceListAsync();
    Task<ProductPriceList> GetPriceByProductId(Guid id);
    void Remove(ProductPriceList priceList);
}
