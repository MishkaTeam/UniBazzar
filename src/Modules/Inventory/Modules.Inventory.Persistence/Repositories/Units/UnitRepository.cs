using BuildingBlocks.Domain.Context;
using BuildingBlocks.Persistence;
using Domain.Aggregates.Units;
using Modules.Inventory.Persistence;

namespace Persistence.Repositories.Aggregates.Units;

public class UnitRepository(InventoryDbContext INVdbcontext, IExecutionContextAccessor executionContextAccessor) 
	: RepositoryBase<Unit>(INVdbcontext, executionContextAccessor), IUnitRepository 
{
	
}
