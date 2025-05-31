namespace BuildingBlocks.Domain.SeedWork;

public interface IEntityHasOwner
{
    public Guid OwnerId { get; }

    public void SetOwner(Guid ownerId);
    public Guid GetOwner();

}
