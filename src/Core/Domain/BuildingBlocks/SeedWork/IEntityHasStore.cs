namespace Domain.BuildingBlocks.SeedWork;

public interface IEntityHasStore
{
    public Guid StoreId { get; }
    public Guid GetStore();
    public void SetStore(Guid StoreId);
}