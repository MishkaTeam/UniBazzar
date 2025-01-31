using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.Products.ProductPriceList.ViewModels;

public class CreateProductPriceListViewModel
{
    [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.ProductId))]
    public Guid ProductId { get; set; }

    [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Price))]
    public int Price { get; set; }
}
