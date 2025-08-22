using BuildingBlocks.Persistence;
using Domain.Aggregates.PriceLists;
using Microsoft.EntityFrameworkCore;
using BuildingBlocks.Persistence.Extensions;
using BuildingBlocks.Domain.Context;
using System.Collections.Generic;

namespace Persistence.Repositories.Aggregates.Products;

public class PriceListsRepository : RepositoryBase<PriceList> ,IPriceListRepository
{

    public PriceListsRepository(UniBazzarContext context, IExecutionContextAccessor execution) : base(context, execution)
    {
    }

    public async Task<List<(Guid productId, decimal price)>> GetPrice(List<Guid> guids)
    {
        var prices = await DbSet
            .SelectMany(p => p.Items)
            .Where(item => guids.Contains(item.ProductId))
            .Select(item => new { item.ProductId, item.Price })
            .ToListAsync(); // از EF به LINQ to Objects سوییچ می‌کنیم

        return [.. prices.Select(x => (x.ProductId, x.Price))];
    }

    public Task<PriceList> GetPriceListItems(Guid id)
    {
        return DbSet
               .Include(x => x.Items)
               .ThenInclude(x => x.Product)
               .Where(x => x.Id == id)
               .StoreFilter(ExecutionContext.StoreId)
               .FirstOrDefaultAsync();
    }

    public override async Task<IEnumerable<PriceList>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            // Check if StoreId is empty (default value)
            var storeId = ExecutionContext.StoreId;
            var isDefaultStoreId = storeId == Guid.Parse("00000000-0000-0000-0000-000000000000");
            
            if (isDefaultStoreId)
            {
                // If StoreId is default, get all PriceLists without filter

                return (List<PriceList>?)await DbSet
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);
            }
            
            // First try with store filter
            var result = await DbSet
                .StoreFilter(ExecutionContext.StoreId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            // If no results, try without store filter for development/testing
            if (!result.Any())
            {
                result = await DbSet
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);
            }

            return result;
        }
        catch (Exception ex)
        {
            // Log the error and return empty list
            // In production, you might want to re-throw or handle differently
            return new List<PriceList>();
        }
    }
}
