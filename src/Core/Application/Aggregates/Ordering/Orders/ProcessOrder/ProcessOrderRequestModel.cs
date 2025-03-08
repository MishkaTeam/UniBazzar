namespace Application.Aggregates.Ordering.Orders.ProcessOrder;

public class ProcessOrderRequestModel
{
    public Guid BasketId { get; set; }
}

public class ProcessOrderResponseModel
{
    public Guid OrderId { get; set; }
    public long OrderReferenceNumber { get; set; }
}