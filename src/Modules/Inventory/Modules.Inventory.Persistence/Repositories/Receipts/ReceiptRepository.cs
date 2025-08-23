using BuildingBlocks.Domain.Context;
using BuildingBlocks.Persistence;
using Microsoft.EntityFrameworkCore;
using Modules.Inventory.Domain.Aggreates.Receipts;
using Modules.Inventory.Domain.Aggreates.Receipts.ReceiptItems;

namespace Modules.Inventory.Persistence.Repositories.Receipts;

public class ReceiptRepository(InventoryDbContext INVdbcontext, IExecutionContextAccessor executionContextAccessor)
      : RepositoryBase<Receipt>(INVdbcontext, executionContextAccessor), IReceiptRepository
{
    public async Task<List<ReceiptItem>> GetAllItems(Guid receiptId)
    {
        var receipt = await DbSet.Include(x => x.ReceiptItem).FirstOrDefaultAsync(x => x.Id == receiptId);

        return receipt.ReceiptItem;
    }
}
