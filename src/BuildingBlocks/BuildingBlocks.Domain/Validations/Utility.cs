namespace BuildingBlocks.Domain.Validations;

public static class Utility
{
    public static Guid RequierdGuid(this Guid value, string name)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException
                ($"{name} cannot be empty.", nameof(name));
        }

        return value;
    }

    public static int NotNegativeInt(this int value, string name)
    {
        if (value < 0)
        {
            throw new ArgumentException
                ($"{name} cannot be negative.", nameof(name));
        }

        return value;
    }
}
