namespace Modules.Treasury.Application.Contracts;

public sealed record ReceiptCustomer
{
    public ReceiptCustomer(Guid customerId, string customerName)
    {
        CustomerId = customerId;
        CustomerName = customerName;
    }

    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; }
}
