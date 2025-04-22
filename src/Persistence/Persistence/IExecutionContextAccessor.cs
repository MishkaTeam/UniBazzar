namespace Persistence;

public interface IExecutionContextAccessor
{
   public Guid UserId { get; set; }
   public Guid StoreId { get; set; }
}