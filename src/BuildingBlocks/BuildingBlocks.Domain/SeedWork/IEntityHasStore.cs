namespace BuildingBlocks.Domain.SeedWork;

public interface IEntityHasStore
{
    public Guid StoreId { get; }
    public Guid GetStore();
    public void SetStore(Guid StoreId);
}