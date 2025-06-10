namespace Application.Aggregates.Ordering.Baskets.ViewModels.InitializeBasket;

public class InitializeBasketViewModel
{
    public InitializeBasketViewModel(Guid id, string referenceNumber)
    {
        Id = id;
        ReferenceNumber = referenceNumber;
    }


    public Guid Id { get; set; }
    public string ReferenceNumber { get; set; }

}