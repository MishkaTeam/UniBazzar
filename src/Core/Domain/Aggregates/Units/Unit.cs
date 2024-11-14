using Ardalis.GuardClauses;
using BuildingBlocks.Domain.Aggregates;

namespace Domain.Aggregates.Units;

public class Unit : Entity
{
    public Unit()
    {
        // FOR EF!
    }

    public static Unit Create(string title, Unit? parent, decimal ratio = 1m)
    {
        var unit = new Unit(title, parent, ratio);

        ValidateRatio(parent, ratio, unit);

        return unit;
    }

    public string Title { get; private set; }
    public Unit? Parent { get; private set; }
    public decimal Ratio { get; private set; }

    private Unit(string title, Unit? parent, decimal ratio)
    {
        Title = title;
        Parent = parent;
        Ratio = ratio;
    }

    private static void ValidateRatio(Unit? parent, decimal ratio, Unit unit)
    {
        if (parent == null)
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
