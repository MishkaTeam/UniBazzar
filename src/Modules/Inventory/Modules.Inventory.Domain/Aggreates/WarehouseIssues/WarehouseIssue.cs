using BuildingBlocks.Domain.Aggregates;
using Domain.Aggregates.Customers;
using Modules.Inventory.Domain.Aggreates.Cardexs;
using Modules.Inventory.Domain.Aggreates.Cardexs.Enums;
using Modules.Inventory.Domain.Aggreates.Receipts.ReceiptItems;
using Modules.Inventory.Domain.Aggreates.WarehouseIssues.WarehouseIssueItems;
using Modules.Inventory.Domain.Aggreates.Warehouses;

namespace Modules.Inventory.Domain.Aggreates.WarehouseIssues
{
    public class WarehouseIssue : Entity
    {
        public Guid CustomerId { get; set; }

        public Guid WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }

        public List<WarehouseIssueItem> WarehouseIssueItems { get; set; }

        public string? Description { get; set; }

        public string WarehouseIssueNumber { get; set; }

        public DateTime WarehouseIssueDate { get; set; }

        private WarehouseIssue(Guid customerId, Guid warehouseId, string? description, string warehouseIssueNumber, DateTime warehouseIssueDate)
        {
            CustomerId = customerId;
            WarehouseId = warehouseId;
            Description = description;
            WarehouseIssueDate = warehouseIssueDate;
            WarehouseIssueNumber = warehouseIssueNumber;
        }

        public static WarehouseIssue Create(Guid customerId, Guid warehouseId, string? description, string warehouseIssueNumber, DateTime warehouseIssueDate)
        {
            return new WarehouseIssue(customerId, warehouseId, description, warehouseIssueNumber, warehouseIssueDate);
        }

        public void Update(Guid customerId, Guid warehouseId, string? description, string warehouseIssueNumber, DateTime warehouseIssueDate)
        {
            CustomerId = customerId;
            WarehouseId = warehouseId;
            Description = description;
            WarehouseIssueDate = warehouseIssueDate;
            WarehouseIssueNumber = warehouseIssueNumber;
        }

        public Cardex AddItem(WarehouseIssueItem warehouseIssueItem)
        {
            WarehouseIssueItems.Add(warehouseIssueItem);

            var cardex = Cardex.Create(WarehouseId, warehouseIssueItem.ProductId, warehouseIssueItem.UnitId, warehouseIssueItem.UnitPrice, CardexType.Receipt,
                          WarehouseIssueNumber, 0, warehouseIssueItem.Quantity, warehouseIssueItem.Description, DateTime.Now);

            return cardex;
        }
    }
}
