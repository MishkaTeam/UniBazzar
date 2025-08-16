using BuildingBlocks.Domain.Data;

namespace Domain.Aggregates.Products;

public interface IProductRepository : IRepositoryBase<Product>
{
    Task<List<Product>> GetFullProductData(string? categorySlug, CancellationToken cancellationToken = default);
    Task<List<Product>> GetFullProductData(List<Guid> productIds, CancellationToken cancellationToken = default);
    Task<List<Product>> GetFullProductData(CancellationToken cancellationToken = default);
    Task<Product> GetFullProductData(string sku);
}