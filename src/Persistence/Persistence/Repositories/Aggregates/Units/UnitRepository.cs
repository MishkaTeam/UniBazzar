using BuildingBlocks.Persistence;
using Domain.Aggregates.Units;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.Aggregates.Units;

public class UnitRepository(UniBazzarContext uniBazzarContext , IExecutionContextAccessor executionContextAccessor) 
	: RepositoryBase<Unit>(uniBazzarContext , executionContextAccessor), IUnitRepository 
{
	
}
