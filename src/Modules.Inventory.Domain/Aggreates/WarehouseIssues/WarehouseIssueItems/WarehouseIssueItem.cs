using BuildingBlocks.Domain.Aggregates;
using Domain.Aggregates.Products;
using Domain.Aggregates.Units;

namespace Modules.Inventory.Domain.Aggreates.WarehouseIssues.WarehouseIssueItems
{
    public class WarehouseIssueItem : Entity
    {
        public Guid WarehouseIssueId { get; set; }
        public WarehouseIssue WarehouseIssue { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid UnitId { get; set; }
        public Unit Unit { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Quantity { get; set; }

        public string? BatchNumber { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public string? Description { get; set; }

        private WarehouseIssueItem(Guid warehouseIssueId, Guid productId, Guid unitId, decimal unitPrice, decimal quantity, string? batchNumber, DateTime? expiryDate, string? descriptionn)
        {
            WarehouseIssueId = warehouseIssueId;
            ProductId = productId;
            UnitId = unitId;
            UnitPrice = unitPrice;
            Quantity = quantity;
            BatchNumber = batchNumber;
            ExpiryDate = expiryDate;
            Description = descriptionn;
        }

        public static WarehouseIssueItem Create(Guid warehouseIssueId, Guid productId, Guid unitId, decimal unitPrice, decimal quantity, string? batchNumber, DateTime? expiryDate, string? descriptionn)
        {
            return new WarehouseIssueItem(warehouseIssueId, productId, unitId, unitPrice, quantity, batchNumber, expiryDate, descriptionn);
        }

        public void Update(Guid warehouseIssueId, Guid productId, Guid unitId, decimal unitPrice, decimal quantity, string? batchNumber, DateTime? expiryDate, string? descriptionn)
        {
            WarehouseIssueId = warehouseIssueId;
            ProductId = productId;
            UnitId = unitId;
            UnitPrice = unitPrice;
            Quantity = quantity;
            BatchNumber = batchNumber;
            ExpiryDate = expiryDate;
            Description = descriptionn;
        }
    }
}
