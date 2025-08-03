using Domain.Aggregates.Cms.HomeViews;
using Resources;
using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.HomeViews.ViewModels.ProductViewItems;

public class ProductViewItemViewModel : UpdateProductViewItemViewModel
{
    public ProductViewItemViewModel()
    {
    }


    [Display
        (ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Product))]
    public string? ProductName { get; set; }

    internal static ProductViewItemViewModel FromProductViewItem(ProductViewItem productViewItem)
    {
        return new ProductViewItemViewModel()
        {
            Id = productViewItem.Id,
            HomeViewId = productViewItem.HomeViewId,
            ProductId = productViewItem.ProductId,
            Ordering = productViewItem.Ordering,
            ProductName = productViewItem?.Product?.Name,
        };
    }

    internal static List<ProductViewItemViewModel> FromProductViewItemList(List<ProductViewItem> productViewItems)
    {
        return productViewItems.Select(x => new ProductViewItemViewModel()
        {
            Id = x.Id,
            HomeViewId = x.HomeViewId,
            ProductId = x.ProductId,
            Ordering = x.Ordering,
            ProductName = x.Product.Name,
        }).ToList();
    }
}
