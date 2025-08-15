using BuildingBlocks.Domain.Context;
using BuildingBlocks.Persistence;
using Modules.Inventory.Domain.Aggreates.Receipts;

namespace Modules.Inventory.Persistence.Repositories.Receipts;

public class ReceiptRepository(InventoryDbContext INVdbcontext, IExecutionContextAccessor executionContextAccessor)
      : RepositoryBase<Receipt>(INVdbcontext, executionContextAccessor), IReceiptRepository
{
}
