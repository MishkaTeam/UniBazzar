namespace Application.Aggregates.Ordering.Baskets.ViewModels.InitializeBasket;

public class InitializeBasketViewModel
{
    public InitializeBasketViewModel(string referenceNumber)
    {
        ReferenceNumber = referenceNumber;
    }

    public string ReferenceNumber { get; set; }
}