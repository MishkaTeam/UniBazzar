using Domain.BuildingBlocks.Data;

namespace Domain.Aggregates.Products;

public interface IProductRepository : IRepositoryBase<Product>
{
    Task<List<Product>> GetFullProductData(CancellationToken cancellationToken = default);
    Task<Product> GetFullProductData(string sku);
}