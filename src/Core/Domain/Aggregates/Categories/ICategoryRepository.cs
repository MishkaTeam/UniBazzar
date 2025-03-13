namespace Domain.Aggregates.Categories;

public interface ICategoryRepository
{
    Task AddCategoryAsync(Category entity);
    Task<Category?> GetCategoryAsync(Guid id);
    Task<List<Category>> GetAllCategoriesAsync();
    Task<List<Category>> GetRootCategoriesAsync();
    //Task<List<Category>> GetMenuCategoriesAsync();
    Task<List<Category>> GetSubCategoriesAsync(Guid parentId);
    Task<int> GetSubCategoriesCountAsync(Guid parentId);
    void RemoveCategory(Category entity);
}