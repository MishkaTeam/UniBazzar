namespace Modules.Wallet.Domain.ValueObjects;

// Domain/ValueObjects/TransactionId.cs
public record TransactionId(Guid Value)
{
    public static TransactionId CreateNew() => new(Guid.NewGuid());
}
