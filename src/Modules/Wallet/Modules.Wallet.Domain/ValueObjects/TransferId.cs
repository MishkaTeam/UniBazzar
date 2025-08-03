namespace Modules.Wallet.Domain.ValueObjects;

// Domain/ValueObjects/TransferId.cs
public record TransferId(Guid Value)
{
    public static TransferId CreateNew() => new(Guid.NewGuid());
}