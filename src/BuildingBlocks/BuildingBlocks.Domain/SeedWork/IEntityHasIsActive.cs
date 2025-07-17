namespace BuildingBlocks.Domain.SeedWork;

public interface IEntityHasIsActive
{
    bool IsActive { get; }
    long DeactivateDate { get; }
    void Activate();
    void Deactivate();
    void SetDeactivateTime();
    void RemoveDeactivateTime();
}
