using BuildingBlocks.Persistence;
using Domain.Aggregates.Categories;
using Microsoft.EntityFrameworkCore;
using BuildingBlocks.Persistence.Extensions;
using BuildingBlocks.Domain.Context;

namespace Persistence.Repositories.Aggregates.Categories;

public class CategoryRepository
    (UniBazzarContext uniBazzarContext, IExecutionContextAccessor contextAccessor)
    : RepositoryBase<Category>(uniBazzarContext, contextAccessor), ICategoryRepository
{
    public async Task<List<Category>> GetAllWithIncludeAsync()
    {
        return await uniBazzarContext.Categories
                    .StoreFilter(contextAccessor.StoreId)
                    .AsNoTracking()
                    .Include(x => x.Parent)
                    .ToListAsync();
    }

    public async Task<List<Category>> GetCurrentStoreCategoriesAsync()
    {
        return await uniBazzarContext.Categories
                    .Include(x => x.Parent)
                    .ThenInclude(x => x.Parent)
                    .ThenInclude(x => x.Parent)
                    .StoreFilter(contextAccessor.StoreId)
                    .AsSplitQuery()
                    .AsNoTracking()
                    .ToListAsync();

    }

    public async Task<List<Category>> GetRootCategoriesAsync()
    {
        var user = contextAccessor.UserId;
        var store = contextAccessor.StoreId;

        return await uniBazzarContext.Categories
                    .StoreFilter(contextAccessor.StoreId)
                    .Where(x => x.ParentId == null || x.ParentId == Guid.Empty)
                    .AsNoTracking()
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