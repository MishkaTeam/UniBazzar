namespace Application.Aggregates.Orders.ViewModels;

public class InitializeBasketViewModel
{
    public InitializeBasketViewModel(long referenceNumber)
    {
        ReferenceNumber = referenceNumber;
    }

    public long ReferenceNumber { get; set; }
}