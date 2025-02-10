using Domain.Aggregates.Categories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Aggregates.Categories;

public class CategoryRepository
	(UniBazzarContext uniBazzarContext) : ICategoryRepository
{
	public async Task AddCategoryAsync(Category entity)
	{
		await uniBazzarContext.AddAsync(entity);
	}

	public async Task<List<Category>> GetAllCategoriesAsync()
	{
		return await uniBazzarContext.Categories
					.Include(x => x.Parent)
					.ToListAsync();
	}

	public async Task<List<Category>> GetRootCategoriesAsync()
	{
		return await uniBazzarContext.Categories
					.Where(x => x.ParentId == null || x.ParentId == Guid.Empty)
					.ToListAsync();
	}

	public async Task<Category?> GetCategoryAsync(Guid id)
	{
		var category = await uniBazzarContext.Categories
					.FirstOrDefaultAsync(x => x.Id == id);

		return category;
	}

	public async Task<List<Category>> GetSubCategoriesAsync(Guid parentId)
	{
		return await uniBazzarContext.Categories
					.Include(x => x.Parent)
					.Where(x => x.ParentId == parentId)
					.ToListAsync();
	}

	public async Task<int> GetSubCategoriesCountAsync(Guid parentId)
	{
		return await uniBazzarContext.Categories
					.Where(x => x.ParentId == parentId)
					.CountAsync();
	}

	public void RemoveCategory(Category entity)
	{
		uniBazzarContext.Remove(entity);
	}
}