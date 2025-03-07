namespace Domain.Aggregates.Products.ProductPriceLists;

public interface IProductPriceListRepository
{
    Task AddProductPriceList(ProductPriceList productPriceList);
    Task<ProductPriceList> GetProductPriceListAsync(Guid id);
    Task<List<ProductPriceList>> GetAllProductPriceListAsync();
    Task<ProductPriceList> GetPriceByProductId(Guid id);
    Task<List<ProductPriceList>> GetPriceListByProductId(Guid productid);
    void RemovePriceList(ProductPriceList priceList);
}
