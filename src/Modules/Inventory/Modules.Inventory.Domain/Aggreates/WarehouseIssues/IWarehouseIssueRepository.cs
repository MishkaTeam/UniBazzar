using BuildingBlocks.Domain.Data;
using Modules.Inventory.Domain.Aggreates.WarehouseIssues.WarehouseIssueItems;

namespace Modules.Inventory.Domain.Aggreates.WarehouseIssues
{
    public interface IWarehouseIssueRepository : IRepositoryBase<WarehouseIssue>
    {
        Task<List<WarehouseIssueItem>> GetWarehouseIssueItems(Guid warehouseIssueId);
    }
}
