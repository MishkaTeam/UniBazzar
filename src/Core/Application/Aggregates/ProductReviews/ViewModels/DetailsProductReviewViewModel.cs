using Application.Aggregates.Customers;
using Application.Aggregates.Products.ViewModels;

namespace Application.Aggregates.ProductReviews.ViewModels;

public class DetailsProductReviewViewModel : UpdateProductReviewViewModel
{
    public CustomerViewModel Customer { get; set; }
    public ProductViewModel Product { get; set; }
}
