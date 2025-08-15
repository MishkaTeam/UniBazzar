using BuildingBlocks.Domain.Context;
using BuildingBlocks.Persistence;
using Modules.Inventory.Domain.Aggreates.Cardexs;

namespace Modules.Inventory.Persistence.Repositories.Cardexs;

public class CardexRepository(InventoryDbContext INVdbcontext, IExecutionContextAccessor executionContextAccessor)
      : RepositoryBase<Cardex>(INVdbcontext, executionContextAccessor), ICardexRepository
{

}
