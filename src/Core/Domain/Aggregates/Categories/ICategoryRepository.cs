namespace Domain.Aggregates.Categories;

public interface ICategoryRepository
{
	Task AddCategoryAsync(Category entity);
	Task<Category> GetCategoryAsync(Guid id);
	Task<List<Category>> GetAllCategoriesAsync();
	Task<List<Category>> GetSubCategoriesAsync(Guid parentId);
	void RemoveCategory(Category entity);
}