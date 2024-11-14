using System;

namespace BuildingBlocks.Domain.SeedWork;

public interface IEntityHasTenant
{
    public Guid TenantId { get; }

    public void SetTenant(Guid tenantId);
    public Guid GetTenant();

}
