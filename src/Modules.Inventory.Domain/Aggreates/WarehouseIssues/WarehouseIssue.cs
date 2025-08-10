using Domain.Aggregates.Customers;

namespace Modules.Inventory.Domain.Aggreates.WarehouseIssues
{
    public class WarehouseIssue
    {
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        public Guid InventoryId { get; set; }
        public Inventories.Inventory Inventory { get; set; }

        public string? Description { get; set; }

        public string WarehouseIssueNumber { get; set; }

        public DateTime WarehouseIssueDate { get; set; }

        private WarehouseIssue(Guid customerId, Guid inventoryId, string? description, string warehouseIssueNumber, DateTime warehouseIssueDate)
        {
            CustomerId = customerId;
            InventoryId = inventoryId;
            Description = description;
            WarehouseIssueDate = warehouseIssueDate;
            WarehouseIssueNumber = warehouseIssueNumber;
        }

        public static WarehouseIssue Create(Guid customerId, Guid inventoryId, string? description, string warehouseIssueNumber, DateTime warehouseIssueDate)
        {
            return new WarehouseIssue(customerId, inventoryId, description, warehouseIssueNumber, warehouseIssueDate);
        }

        public void Update(Guid customerId, Guid inventoryId, string? description, string warehouseIssueNumber, DateTime warehouseIssueDate)
        {
            CustomerId = customerId;
            InventoryId = inventoryId;
            Description = description;
            WarehouseIssueDate = warehouseIssueDate;
            WarehouseIssueNumber = warehouseIssueNumber;
        }
    }
}
