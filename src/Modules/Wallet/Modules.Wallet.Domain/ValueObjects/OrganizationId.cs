namespace Modules.Wallet.Domain.ValueObjects;

// Domain/ValueObjects/OrganizationId.cs
public record OrganizationId(Guid Value)
{
    public static OrganizationId CreateNew() => new(Guid.NewGuid());
}
