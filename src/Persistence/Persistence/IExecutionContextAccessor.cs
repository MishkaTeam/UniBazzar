namespace Persistence;

public interface IExecutionContextAccessor
{
    public Guid? UserId { get; }
    public Guid StoreId { get; }
    string Role { get; }
}