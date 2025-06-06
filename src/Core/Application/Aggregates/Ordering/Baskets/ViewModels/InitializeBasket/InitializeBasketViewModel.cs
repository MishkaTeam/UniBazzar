namespace Application.Aggregates.Orders.ViewModels;

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