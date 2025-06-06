using Domain.Aggregates.Products.ProductFeatures;
using Domain.Aggregates.Products.ProductImages;

namespace Application.Aggregates.Products.ViewModels;

public class ProductDetailViewModel
{
    public Guid Id { get; internal set; }
    public decimal Price { get; internal set; }
    public List<ProductImage> Images { get; internal set; }
    public string Name { get; internal set; }
    public string SKU { get; internal set; }
    public string Slug { get; internal set; }
    public decimal PriceAfterDiscount { get; set; }
    public decimal Discount { get; set; }
    public long TotalVoters { get; set; }
    public long TotalRate { get; set; }
    public string ShortDescription { get; internal set; }
    public string FullDescription { get; internal set; }
    public List<ProductFeature> ProductFeatures { get; internal set; }
}
