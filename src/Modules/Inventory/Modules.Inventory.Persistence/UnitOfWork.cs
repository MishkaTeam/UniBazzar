using BuildingBlocks.Domain.Data;

namespace Modules.Inventory.Persistence;

public class UnitOfWork(InventoryDbContext dbContext) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken? cancellationToken = null)
    {
        if (cancellationToken.HasValue)
            return dbContext.SaveChangesAsync(cancellationToken.Value);
        else
            return dbContext.SaveChangesAsync();
    }
}
