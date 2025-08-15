using BuildingBlocks.Domain.Data;
using Modules.Inventory.Domain.Aggreates.Receipts.ReceiptItems;

namespace Modules.Inventory.Domain.Aggreates.Receipts
{
    public interface IReceiptRepository : IRepositoryBase<Receipt>
    {
        Task<List<ReceiptItem>> GetAllReceiptItem(Guid receiptId);
    }
}
