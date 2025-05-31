namespace Modules.Treasury.Application.Contracts;

public sealed record Customer
{
    public Customer(Guid customerId, string customerName)
    {
        CustomerId = customerId;
        CustomerName = customerName;
    }

    public required Guid CustomerId { get; set; }
    public required string CustomerName { get; set; }
}
