using BuildingBlocks.Domain.Aggregates;
using Domain.Aggregates.Products;
using Domain.Aggregates.Units;

namespace Modules.Inventory.Domain.Aggreates.Receipts.ReceiptItems
{
    public class ReceiptItem : Entity
    {
        public Guid ReceiptId { get; set; }
        public Receipt Receipt { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid UnitId { get; set; }
        public Unit Unit { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Quantity { get; set; }

        public string? Location { get; set; }

        public string? BatchNumber { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public string? Description { get; set; }

        private ReceiptItem(Guid receiptId, Guid productId, Guid unitId, decimal unitPrice, decimal quantity, 
                            string? batchNumber, DateTime? expiryDate, string? description)
        {
            UnitId = unitId;
            Quantity = quantity;
            ReceiptId = receiptId;
            ProductId = productId;
            UnitPrice = unitPrice;
            ExpiryDate = expiryDate;
            BatchNumber = batchNumber;
            Description = description;
        }

        public static ReceiptItem Create(Guid receiptId, Guid productId, Guid unitId, decimal unitPrice, decimal quantity,
                            string? batchNumber, DateTime? expiryDate, string? description)
        {
            return new ReceiptItem(receiptId, productId, unitId, unitPrice, quantity, batchNumber, expiryDate, description);
        }

        public void Update(Guid receiptId, Guid productId, Guid unitId, decimal unitPrice, decimal quantity,
                            string? batchNumber, DateTime? expiryDate, string? description)
        {
            UnitId = unitId;
            Quantity = quantity;
            ReceiptId = receiptId;
            ProductId = productId;
            UnitPrice = unitPrice;
            ExpiryDate = expiryDate;
            BatchNumber = batchNumber;
            Description = description;
        }
    }
}
