using BuildingBlocks.Domain.Aggregates;
using Domain.Aggregates.Customers;
using Modules.Inventory.Domain.Aggreates.Warehouses;

namespace Modules.Inventory.Domain.Aggreates.WarehouseIssues
{
    public class WarehouseIssue : Entity
    {
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        public Guid WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }

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
    }
}
