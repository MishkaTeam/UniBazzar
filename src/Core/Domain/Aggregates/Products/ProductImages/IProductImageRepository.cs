using Domain.BuildingBlocks.Data;

namespace Domain.Aggregates.Products.ProductImages;

public interface IProductImageRepository : IRepositoryBase<ProductImage>
{
    Task<List<ProductImage>> GetImageByProductIdAsync(Guid productid);
}
