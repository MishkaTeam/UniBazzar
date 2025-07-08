using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.ProductReviews.ViewModels;

public class CreateProductReviewViewModel
{
    [Required]
    [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Text))]
    public string Text { get; set; }
    [Required]
    public byte Rate { get; set; }

    public Guid CustomerId { get; set; }

    public Guid ProductId { get; set; }

    public bool IsVerified { get; set; }

    public long InsertDateTime { get; set; }
    public string InsertDateTimeFa
    {
        get
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(InsertDateTime).ToString("yyyy/MM/dd HH:ss", new System.Globalization.CultureInfo("fa-IR"));
        }
    } 
}
