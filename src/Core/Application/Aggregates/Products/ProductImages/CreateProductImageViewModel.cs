using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.Products.ProductImages;

public class CreateProductImageViewModel
{
    [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.ProductId))]
    public Guid ProductId { get; set; }

    [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Image))]
    public string ImageUrl { get; set; }
}
