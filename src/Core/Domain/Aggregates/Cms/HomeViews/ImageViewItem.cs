using BuildingBlocks.Domain.Aggregates;
using BuildingBlocks.Domain.Validations;
using Framework.DataType;

namespace Domain.Aggregates.Cms.HomeViews;

public class ImageViewItem : Entity
{
    private ImageViewItem()
    {
        // FOR EF!
    }

    private ImageViewItem
        (Guid homeViewId, string title, string imageUrl, string? navigationUrl, string column, int ordering)
    {
        HomeViewId = homeViewId;
        Title = title;
        ImageUrl = imageUrl;
        NavigationUrl = navigationUrl;
        Column = column;
        Ordering = ordering;
    }


    public string Title { get; private set; }
    public string ImageUrl { get; private set; }
    public string? NavigationUrl { get; private set; }
    public string Column { get; private set; }

    public Guid HomeViewId { get; private set; }

    public static ImageViewItem Create
        (Guid homeViewId, string title, string imageUrl, string? navigationUrl, string column, int ordering)
    {
        var imageViewItem = new ImageViewItem(
            homeViewId.RequierdGuid(nameof(HomeViewId)),
            title.Fix() ?? "",
            imageUrl.Fix() ?? "",
            navigationUrl.Fix() ?? "",
            column.Fix() ?? "",
            ordering.NotNegativeInt(nameof(Ordering)));

        return imageViewItem;
    }
}
