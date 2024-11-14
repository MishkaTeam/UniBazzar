namespace BuildingBlocks.Domain.SeedWork;

public interface IEntity :
     IsEntityHasVersionControl
     , IEntityHasUpdateInfo
     , IEntityHasTenant
     , IEntityHasOwner
     , IEntityHasStore

{
    public Guid Id { get; }
    public int Ordering { get; }
    public long InsertDateTime { get; }
    public Guid InsertedBy { get; }
    void SetInsertDateTime();
    void SetInsertBy(Guid Id);
}
