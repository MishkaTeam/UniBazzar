using BuildingBlocks.Domain.Context;
using BuildingBlocks.Persistence;
using Modules.Inventory.Domain.Aggreates.Suppliers;

namespace Modules.Inventory.Persistence.Repositories.Suppliersl;

public class SupplierRepository(InventoryDbContext INVdbContext, IExecutionContextAccessor executionContextAccessor)
      : RepositoryBase<Supplier>(INVdbContext, executionContextAccessor), ISupplierRepository
{
}
