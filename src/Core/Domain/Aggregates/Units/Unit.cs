using Ardalis.GuardClauses;
using Entity = Domain.BuildingBlocks.Aggregates.Entity;

namespace Domain.Aggregates.Units;

public class Unit : Entity
{
    public Unit()
    {
        // FOR EF!
    }

    public static Unit Create(string title, Guid? baseUnitId, decimal ratio = 1m)
    {
		var unit = new Unit(title, baseUnitId, ratio)
		{
			BaseUnitId = ValidateBaseUnit(baseUnitId)
		};
		ValidateRatio(baseUnitId, ratio, unit);

        return unit;
    }

	public void Update(string title, Guid? baseUnitId, decimal ratio = 1m)
	{
		Title = title;
		BaseUnitId = ValidateBaseUnit(baseUnitId);
		Ratio = ratio;

		SetUpdateDateTime();
	}

	private static Guid? ValidateBaseUnit(Guid? baseUnitId)
    {
        if (baseUnitId == Guid.Empty)
        {
            baseUnitId = null;
        }

        return baseUnitId;
    }

    public string Title { get; private set; }
    public Guid? BaseUnitId { get; private set; }
    public Unit? BaseUnit { get; private set; }
    public decimal Ratio { get; private set; }

    private Unit(string title, Guid? baseUnitId, decimal ratio)
    {
        Title = title;
        BaseUnitId = baseUnitId;
        Ratio = ratio;
    }

    private static void ValidateRatio(Guid? baseUnitId, decimal ratio, Unit unit)
    {
        if (baseUnitId == null || baseUnitId == Guid.Empty)
        {
            unit.Ratio = 1m;
        }
        else
        {
            var message = string.Format(
                Resources.Messages.Validations.GreaterThan
                ,Resources.DataDictionary.UnitRatio, "0");

            Guard.Against.NegativeOrZero(ratio, 
                    exceptionCreator: () => throw new ArgumentException(message: message));
        }
    }


}
