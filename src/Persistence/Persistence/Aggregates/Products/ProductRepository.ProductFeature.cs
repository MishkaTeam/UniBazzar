using Domain.Aggregates.Products.ProductFeatures;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Aggregates.Products;

public partial class ProductRepository
{
	public void AddProductFeature(ProductFeature entity)
	{
		uniBazzarContext.Add(entity);
	}

	public Task<List<ProductFeature>> GetAllProductFeaturesAsync(Guid productId)
	{
		return uniBazzarContext.ProductFeatures
							   .Include(x => x.Product)
							   .Where(x => x.ProductId == productId)
							   .ToListAsync();
	}

	public async Task<ProductFeature> GetProductFeatureAsync(Guid id)
	{
		var productFeature = await uniBazzarContext.ProductFeatures
								.Include(x => x.Product)
								.FirstOrDefaultAsync(x => x.Id == id);

		return productFeature ?? new ProductFeature();
	}

	public void RemoveProductFeature(ProductFeature entity)
	{
		uniBazzarContext.Remove(entity);
	}
}