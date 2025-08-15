using BuildingBlocks.Persistence;
using Domain.Aggregates.Categories;
using Microsoft.EntityFrameworkCore;
using BuildingBlocks.Persistence.Extensions;
using BuildingBlocks.Domain.Context;
using Modules.Inventory.Persistence;

namespace Persistence.Repositories.Aggregates.Categories;

public class CategoryRepository
    (InventoryDbContext INVdbcontext, IExecutionContextAccessor contextAccessor)
    : RepositoryBase<Category>(INVdbcontext, contextAccessor), ICategoryRepository
{
    public async Task<List<Category>> GetAllWithIncludeAsync()
    {
        return await DbSet
                    .StoreFilter(contextAccessor.StoreId)
                    .AsNoTracking()
                    .Include(x => x.Parent)
                    .ToListAsync();
    }

    public async Task<List<Category>> GetCurrentStoreCategoriesAsync()
    {
        return await DbSet
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

        return await DbSet
                    .StoreFilter(contextAccessor.StoreId)
                    .Where(x => x.ParentId == null || x.ParentId == Guid.Empty)
                    .AsNoTracking()
                    .ToListAsync();

    }

    public async Task<List<Category>> GetSubCategoriesAsync(Guid parentId)
    {
        return await DbSet
                    .Include(x => x.Parent)
                    .Where(x => x.ParentId == parentId)
                    .ToListAsync();
    }

    public async Task<int> GetSubCategoriesCountAsync(Guid parentId)
    {
        return await DbSet
                    .Where(x => x.ParentId == parentId)
                    .CountAsync();
    }
}