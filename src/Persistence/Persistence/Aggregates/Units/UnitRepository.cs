using Domain.Aggregates.Units;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Aggregates.Units;

public class UnitRepository(UniBazzarContext uniBazzarContext) : IUnitRepository
{
	public void AddUnit(Unit entity)
	{
		uniBazzarContext.Add(entity);
	}

	public Task<List<Unit>> GetAllUnitsAsync()
	{
		return uniBazzarContext.Units
							   .Include(x => x.BaseUnit)
							   .ToListAsync();
	}

	public Task<List<Unit>> GetRootUnitsAsync()
	{
		return uniBazzarContext.Units
							   .Where(x => x.BaseUnitId == null)
							   .ToListAsync();
	}

	public async Task<Unit> GetUnitAsync(Guid id)
	{
		var unit = await uniBazzarContext.Units
							   .FirstOrDefaultAsync(x => x.Id == id);
		return unit ?? new Unit();
	}

	public void Remove(Unit entity)
	{
		uniBazzarContext.Units.Remove(entity);
	}
}
