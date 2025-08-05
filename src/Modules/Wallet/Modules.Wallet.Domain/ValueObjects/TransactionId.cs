namespace Modules.WalletOps.Domain.ValueObjects;

// Domain/ValueObjects/TransactionId.cs
public record TransactionId(Guid Value)
{
    public static TransactionId CreateNew() => new(Guid.NewGuid());
}
