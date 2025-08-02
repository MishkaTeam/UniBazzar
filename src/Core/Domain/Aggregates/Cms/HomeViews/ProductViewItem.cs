using BuildingBlocks.Domain.Aggregates;
using BuildingBlocks.Domain.Validations;
using Domain.Aggregates.Products;

namespace Domain.Aggregates.Cms.HomeViews;

public class ProductViewItem : Entity
{
    private ProductViewItem()
    {
        // FOR EF!
    }

    private ProductViewItem
        (Guid homeViewId, Guid productId, int ordering)
    {
        HomeViewId = homeViewId;
        ProductId = productId;
        Ordering = ordering;
    }


    public Product Product { get; private set; }
    public Guid ProductId { get; private set; }
    public Guid HomeViewId { get; private set; }

    public static ProductViewItem Create
        (Guid homeViewId, Guid productId, int ordering)
    {
        var productViewItem = new ProductViewItem(
            homeViewId.RequierdGuid(nameof(HomeViewId)),
            productId.RequierdGuid(nameof(ProductId)),
            ordering.NotNegativeInt(nameof(Ordering)));

        return productViewItem;
    }
}
