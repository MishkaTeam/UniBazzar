using BuildingBlocks.Domain.Context;
using BuildingBlocks.Persistence;
using Microsoft.EntityFrameworkCore;
using Modules.Inventory.Domain.Aggreates.Cardexs;
using Modules.Inventory.Domain.Aggreates.WarehouseIssues;
using Modules.Inventory.Domain.Aggreates.WarehouseIssues.WarehouseIssueItems;

namespace Modules.Inventory.Persistence.Repositories.WarehouseIssues;

public class WarehouseIssueRepository(InventoryDbContext INVdbcontext, IExecutionContextAccessor executionContextAccessor)
      : RepositoryBase<WarehouseIssue>(INVdbcontext, executionContextAccessor), IWarehouseIssueRepository
{
    public async Task<List<WarehouseIssueItem>> GetAllWarehouseIssueItem(Guid warehouseIssueId)
    {
        var warehouseIssue = await DbSet.Include(x => x.WarehouseIssueItems)
                             .FirstOrDefaultAsync(x => x.Id == warehouseIssueId);

        return
            warehouseIssue.WarehouseIssueItems;
    }
}
