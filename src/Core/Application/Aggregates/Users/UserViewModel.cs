namespace Application.Aggregates.Users;

public class UserViewModel : UpdateUserViewModel
{
    public Guid StoreId { get; set; }
    public Guid Role { get; set; }
}
