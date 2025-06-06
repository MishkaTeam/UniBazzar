using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.ProductReviews.ViewModels;

public class CreateProductReviewViewModel
{
    [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Text))]
    public string Text { get; set; }
    public byte Rate { get; set; }

    public Guid CustomerId { get; set; }

    public Guid ProductId { get; set; }
}
