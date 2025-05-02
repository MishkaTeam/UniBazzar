namespace Domain.ProductSearch;

public class SuggestionItem
{
    public Guid ProductId { get; set; }
    public Guid StoreId { get; set; }

    public string ProductTitle { get; set; } = "";
    public string? Category { get; set; }
    public bool IsCategory { get; set; } = false;
}
