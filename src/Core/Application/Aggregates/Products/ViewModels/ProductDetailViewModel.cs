using Domain.Aggregates.Products.ProductAttributes;
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
    public string ShortDescription { get; set; }
    public string FullDescription { get; set; }
    public List<string> Images { get; set; }
    public List<ProductFeatureViewModel> ProductFeatures { get; set; }
    public List<ProductAttributeViewModel> ProductAttributes { get; set; }
}
