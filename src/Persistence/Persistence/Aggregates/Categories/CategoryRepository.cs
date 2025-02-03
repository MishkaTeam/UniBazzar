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
		return await uniBazzarContext.Categories.ToListAsync();
	}

	public async Task<Category> GetCategoryAsync(Guid id)
	{
		var category = await uniBazzarContext.Categories
						.FirstOrDefaultAsync(x => x.Id == id);

		return category ?? new Category();
	}

	public async Task<List<Category>> GetSubCategoriesAsync(Guid parentId)
	{
		return await uniBazzarContext.Categories
					.Where(x => x.ParentId == parentId)
					.ToListAsync();
	}

	public void RemoveCategory(Category entity)
	{
		uniBazzarContext.Remove(entity);
	}
}