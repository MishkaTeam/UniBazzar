using BuildingBlocks.Persistence;
using Domain.Aggregates.Categories;
using Domain.Aggregates.Customers;
using Microsoft.EntityFrameworkCore;
using Persistence.Extensions;

namespace Persistence.Aggregates.Categories;

public class CategoryRepository
    (UniBazzarContext uniBazzarContext, IExecutionContextAccessor contextAccessor)
    : RepositoryBase<Category>(uniBazzarContext, contextAccessor), ICategoryRepository
{
    public async Task<List<Category>> GetAllWithIncludeAsync()
    {
        return await 
            uniBazzarContext.Categories
            .StoreFilter(contextAccessor.StoreId)
            .AsNoTracking()
            .Include(x => x.Parent)
            .ToListAsync();
    }

    public async Task<List<Category>> GetRootCategoriesAsync()
    {
        var user = contextAccessor.UserId;
        var store = contextAccessor.StoreId;

        return await uniBazzarContext.Categories
                    .Where(x => x.ParentId == null || x.ParentId == Guid.Empty)
                    .ToListAsync();
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
}