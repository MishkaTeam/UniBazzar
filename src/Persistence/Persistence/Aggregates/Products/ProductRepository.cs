using Domain.Aggregates.Products;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Aggregates.Products;

public partial class ProductRepository
	(UniBazzarContext uniBazzarContext) : IProductRepository
{
	public async Task AddProductAsync(Product entity)
	{
		await uniBazzarContext.AddAsync(entity);
	}

	public async Task<List<Product>> GetAllProductsAsync()
	{
		return await uniBazzarContext.Products
					//.Include(x => x.Store)
					//.Include(x => x.Category)
					.Include(x => x.Unit)
					.ToListAsync();
	}

	public async Task<Product?> GetProductAsync(Guid id)
	{
		var product = await uniBazzarContext.Products
					.FirstOrDefaultAsync(x => x.Id == id);

		return product;
	}

	public void RemoveProduct(Product entity)
	{
		uniBazzarContext.Remove(entity);
	}
}