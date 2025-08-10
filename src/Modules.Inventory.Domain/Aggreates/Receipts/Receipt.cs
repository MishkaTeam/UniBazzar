using BuildingBlocks.Domain.Aggregates;
using Modules.Inventory.Domain.Aggreates.Inventories;
using Modules.Inventory.Domain.Aggreates.Suppliers;

namespace Modules.Inventory.Domain.Aggreates.Receipts
{
    public class Receipt : Entity
    {
        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public Guid InventoryId { get; set; }
        public Inventories.Inventory Inventory { get; set; }

        public string? Description { get; set; }

        public string ReceiptNumber { get; set; }

        public DateTime ReceiptDate { get; set; }

        private Receipt(Guid supplierId, Guid inventoryId, string? description, DateTime receiptDate, string receiptNumber)
        {
            SupplierId = supplierId;
            InventoryId = inventoryId;
            Description = description;
            ReceiptDate = receiptDate;
            ReceiptNumber = receiptNumber;
        }

        public static Receipt Create(Guid supplierId, Guid inventoryId, string? description, DateTime receiptDate, string receiptNumber)
        {
            return new Receipt(supplierId, inventoryId, description, receiptDate, receiptNumber);
        }

        public void Update(Guid supplierId, Guid inventoryId, string? description, DateTime receiptDate, string receiptNumber)
        {
            SupplierId = supplierId;
            InventoryId = inventoryId;
            Description = description;
            ReceiptDate = receiptDate;
            ReceiptNumber = receiptNumber;
        }
    }
}
