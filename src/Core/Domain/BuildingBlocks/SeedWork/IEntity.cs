namespace Domain.BuildingBlocks.SeedWork;

public interface IEntity :
     IsEntityHasVersionControl
     , IEntityHasUpdateInfo
     , IEntityHasOwner
     , IEntityHasStore

{
    public Guid Id { get; }
    public int Ordering { get; }
    public long InsertDateTime { get; }
    public Guid InsertedBy { get; }
    void SetInsertDateTime();
    void SetInsertBy(Guid Id);
    void IncreaseVersion();
    void SetVersionAndIncrease(int oldVersion);
}
