using BuildingBlocks.Domain.Context;
using BuildingBlocks.Persistence;
using Modules.Inventory.Domain.Aggreates.WarehouseIssues;
using Modules.Inventory.Domain.Aggreates.WarehouseIssues.WarehouseIssueItems;
using Modules.Inventory.Domain.Aggreates.Warehouses;

namespace Modules.Inventory.Persistence.Repositories.WarehouseIssues.WarehouseIssueItems;

public class WarehouseIssueItemRepository(InventoryDbContext INVdbcontext, IExecutionContextAccessor executionContextAccessor)
      : RepositoryBase<WarehouseIssueItem>(INVdbcontext, executionContextAccessor), IWarehouseIssueItemRepository
{
}
