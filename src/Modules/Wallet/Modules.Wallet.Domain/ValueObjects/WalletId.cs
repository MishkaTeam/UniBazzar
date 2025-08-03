namespace Modules.Wallet.Domain.ValueObjects;

// Domain/ValueObjects/WalletId.cs
public record WalletId(Guid Value)
{
    public static WalletId CreateNew() => new(Guid.NewGuid());
}
