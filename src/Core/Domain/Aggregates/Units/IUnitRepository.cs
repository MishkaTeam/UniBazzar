
namespace Domain.Aggregates.Units;

public interface IUnitRepository
{
    void AddUnit(Unit entity);
	Task<List<Unit>> GetRootUnitsAsync();
	Task<List<Unit>> GetAllUnitsAsync();
	Task<Unit> GetUnitAsync(Guid id);
	void Remove(Unit entity);
}
