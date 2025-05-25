namespace Application.Aggregates.Ordering.Orders.ProcessOrder;

public class ProcessOrderResponseModel
{
    public Guid OrderId { get; set; }
    public string OrderReferenceNumber { get; set; }
}