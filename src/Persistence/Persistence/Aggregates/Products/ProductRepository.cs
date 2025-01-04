using Domain.Aggregates.Products;
using Domain.Aggregates.Products.ProductImages;
using Domain.Aggregates.Products.ProductPriceLists;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Aggregates.Products;

public partial class ProductRepository(UniBazzarContext uniBazzarContext) : IProductRepository
{
	public void AddProduct(Product entity)
	{
		uniBazzarContext.Add(entity);
	}

	public Task<List<Product>> GetAllProductsAsync()
    {
		return uniBazzarContext.Products
						   .Include(x => x.ActivePriceList)
						   .Include(x => x.Unit)
						   //.Include(x => x.BrandId)
						   .Include(x => x.Category)
						   .Include(x => x.Store)
						   .ToListAsync();
	}

	public async Task<Product> GetProductAsync(Guid id)
    {
		var product = await uniBazzarContext.Products
					.FirstOrDefaultAsync(x => x.Id == id);
		return product ?? new Product();

	}

	public void RemoveProduct(Product entity)
	{
		uniBazzarContext.Remove(entity);
	}
}
