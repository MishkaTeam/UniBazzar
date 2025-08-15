using BuildingBlocks.Domain.Context;
using BuildingBlocks.Persistence;
using Modules.Inventory.Domain.Aggreates.Cardexs;
using Modules.Inventory.Domain.Aggreates.WarehouseIssues;
using Modules.Inventory.Domain.Aggreates.WarehouseIssues.WarehouseIssueItems;

namespace Modules.Inventory.Persistence.Repositories.WarehouseIssues;

public class WarehouseIssueRepository(InventoryDbContext INVdbcontext, IExecutionContextAccessor executionContextAccessor)
      : RepositoryBase<WarehouseIssue>(INVdbcontext, executionContextAccessor), IWarehouseIssueRepository
{
}
