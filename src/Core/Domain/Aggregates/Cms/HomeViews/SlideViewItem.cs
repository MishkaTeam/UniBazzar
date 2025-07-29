using BuildingBlocks.Domain.Aggregates;
using BuildingBlocks.Domain.Validations;
using Framework.DataType;

namespace Domain.Aggregates.Cms.HomeViews;

public class SlideViewItem : Entity
{
    protected SlideViewItem()
    {
        // FOR EF!
    }

    private SlideViewItem
        (Guid homeViewId, string title, string imageUrl, string? navigationUrl, int interval, int ordering)
    {
        HomeViewId = homeViewId;
        Title = title;
        ImageUrl = imageUrl;
        NavigationUrl = navigationUrl;
        Interval = interval;
        Ordering = ordering;
    }


    public string Title { get; private set; }
    public string ImageUrl { get; private set; }
    public string? NavigationUrl { get; private set; }
    public int Interval { get; private set; }

    public Guid HomeViewId { get; private set; }

    public static SlideViewItem Create
        (Guid homeViewId, string title, string imageUrl, string? navigationUrl, int interval, int ordering)
    {
        var slideViewItem = new SlideViewItem(
            homeViewId.RequierdGuid(nameof(HomeViewId)),
            title.Fix() ?? "",
            imageUrl.Fix() ?? "",
            navigationUrl.Fix() ?? "",
            interval.NotNegativeInt(nameof(Interval)),
            ordering.NotNegativeInt(nameof(Ordering)));

        return slideViewItem;
    }

    public void Update
        (string title, string? imageUrl, string? navigationUrl, int interval, int ordering)
    {
        Title = title.Fix() ?? "";
        NavigationUrl = navigationUrl.Fix() ?? "";
        Interval = interval.NotNegativeInt(nameof(Interval));
        Ordering = ordering.NotNegativeInt(nameof(Ordering));

        if (string.IsNullOrWhiteSpace(imageUrl) == false)
        {
            ImageUrl = imageUrl;
        }

        SetUpdateDateTime();
    }
}
