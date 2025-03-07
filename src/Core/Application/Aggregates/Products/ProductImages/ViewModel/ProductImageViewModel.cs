using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.Products.ProductImages.ViewModel;

public class ProductImageViewModel : CreateProductImageViewModel
{
    [Display
    (ResourceType = typeof(Resources.DataDictionary),
    Name = nameof(Resources.DataDictionary.Id))]
    public Guid Id { get; set; }
}
