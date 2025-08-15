using BuildingBlocks.Domain.Context;
using BuildingBlocks.Persistence;
using Modules.Inventory.Domain.Aggreates.Warehouses;

namespace Modules.Inventory.Persistence.Repositories.Warehouses;

public class WarehouseRepository(InventoryDbContext INVdbcontext, IExecutionContextAccessor executionContextAccessor) 
      : RepositoryBase<Warehouse>(INVdbcontext, executionContextAccessor) ,IWarehouseRepository
{
}
