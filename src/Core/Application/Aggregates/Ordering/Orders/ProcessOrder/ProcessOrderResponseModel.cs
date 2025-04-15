namespace Application.Aggregates.Ordering.Orders.ProcessOrder;

public class ProcessOrderResponseModel
{
    public Guid OrderId { get; set; }
    public long OrderReferenceNumber { get; set; }
}