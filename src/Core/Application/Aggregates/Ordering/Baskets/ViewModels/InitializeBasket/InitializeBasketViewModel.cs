namespace Application.Aggregates.Orders.ViewModels;

public class InitializeBasketViewModel
{
    public InitializeBasketViewModel(string referenceNumber)
    {
        ReferenceNumber = referenceNumber;
    }

    public string ReferenceNumber { get; set; }
}