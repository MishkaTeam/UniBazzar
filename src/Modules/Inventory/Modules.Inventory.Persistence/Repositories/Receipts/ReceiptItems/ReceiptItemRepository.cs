using BuildingBlocks.Domain.Context;
using BuildingBlocks.Persistence;
using Modules.Inventory.Domain.Aggreates.Receipts.ReceiptItems;

namespace Modules.Inventory.Persistence.Repositories.Receipts.ReceiptItems;

public class ReceiptItemRepository(InventoryDbContext INVdbcontext, IExecutionContextAccessor executionContextAccessor)
      : RepositoryBase<ReceiptItem>(INVdbcontext, executionContextAccessor), IReceiptItemRepository
{
}
