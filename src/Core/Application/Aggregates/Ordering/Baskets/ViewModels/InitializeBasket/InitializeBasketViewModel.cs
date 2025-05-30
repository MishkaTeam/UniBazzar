namespace Application.Aggregates.Orders.ViewModels;

public class InitializeBasketViewModel
{
    public InitializeBasketViewModel(Guid id, Guid ownerId)
    {
        Id = id;
        OwnerId = ownerId;
    }


    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }

}