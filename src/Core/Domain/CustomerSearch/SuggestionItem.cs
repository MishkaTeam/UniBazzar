namespace Domain.CustomerSearch;

public class SuggestionItem
{
    public Guid CustomerId { get; set; }
    public Guid StoreId { get; set; }

    public string LastName { get; set; } = "";
    public string NationalCode { get; set; } = "";
    public string Mobile { get; set; } = "";
}