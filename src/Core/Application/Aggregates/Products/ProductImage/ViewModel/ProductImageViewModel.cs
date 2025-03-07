using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.Products.ProductImage.ViewModel;

public class ProductImageViewModel
{
    [Display
    (ResourceType = typeof(Resources.DataDictionary),
    Name = nameof(Resources.DataDictionary.Id))]
    public Guid Id { get; set; }
}
