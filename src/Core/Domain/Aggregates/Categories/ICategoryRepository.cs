using Domain.BuildingBlocks.Data;

namespace Domain.Aggregates.Categories;

public interface ICategoryRepository : IRepositoryBase<Category>
{
    Task<List<Category>> GetAllWithIncludeAsync();
    Task<List<Category>> GetRootCategoriesAsync();
    Task<List<Category>> GetSubCategoriesAsync(Guid parentId);
    Task<int> GetSubCategoriesCountAsync(Guid parentId);
}