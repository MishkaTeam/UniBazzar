namespace Domain.BuildingBlocks.SeedWork;

public interface IEntityHasUpdateInfo
{
    long UpdateDateTime { get; }
    public Guid UpdatedBy { get; }

    void SetUpdateDateTime();
    void SetUpdateBy(Guid Id);

}
