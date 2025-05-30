namespace Application.Aggregates.Products.ViewModels;

public class ProductCardViewModel
{
    public string SKU { get; set; }
    public string Slug { get; set; }
    public decimal Price { get; set; }
    public decimal PriceAfterDiscount { get; set; }
    public decimal Discount { get; set; }
    public string? ImageUrl { get; set; }
    public string Name { get; set; }
    public long TotalVoters { get; set; }
}
