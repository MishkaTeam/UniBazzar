using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.Products.ProductImages.ViewModel;

public class CreateProductImageViewModel
{
    [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.ProductId))]
    public Guid ProductId { get; set; }


    [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Picture))]
    public string? ImageUrl { get; set; }
}
