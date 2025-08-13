using BuildingBlocks.Domain.Aggregates;
using Modules.Inventory.Domain.Aggreates.Suppliers;
using Modules.Inventory.Domain.Aggreates.Warehouses;

namespace Modules.Inventory.Domain.Aggreates.Receipts
{
    public class Receipt : Entity
    {
        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public Guid WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }

        public string? Description { get; set; }

        public string ReceiptNumber { get; set; }

        public DateTime ReceiptDate { get; set; }

        private Receipt(Guid supplierId, Guid warehouseId, string? description, DateTime receiptDate, string receiptNumber)
        {
            SupplierId = supplierId;
            WarehouseId = warehouseId;
            Description = description;
            ReceiptDate = receiptDate;
            ReceiptNumber = receiptNumber;
        }

        public static Receipt Create(Guid supplierId, Guid warehouseId, string? description, DateTime receiptDate, string receiptNumber)
        {
            return new Receipt(supplierId, warehouseId, description, receiptDate, receiptNumber);
        }

        public void Update(Guid supplierId, Guid warehouseId, string? description, DateTime receiptDate, string receiptNumber)
        {
            SupplierId = supplierId;
            WarehouseId = warehouseId;
            Description = description;
            ReceiptDate = receiptDate;
            ReceiptNumber = receiptNumber;
        }
    }
}
