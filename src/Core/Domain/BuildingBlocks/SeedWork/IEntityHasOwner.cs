namespace Domain.BuildingBlocks.SeedWork;

public interface IEntityHasOwner
{
    public Guid OwnerId { get; }

    public void SetOwner(Guid ownerId);
    public Guid GetOwner();

}
