using System.Text.RegularExpressions;
using System.Xml.Linq;
using BuildingBlocks.Domain.Aggregates;
using BuildingBlocks.Domain.SeedWork;
using BuildingBlocks.Domain.Validations;
using Domain.Aggregates.Cms.HomeViews.Enums;
using Framework.DataType;

namespace Domain.Aggregates.Cms.HomeViews;

public class HomeView : Entity,
    IEntityHasIsActive, IEntityHasIsSystemic
{
    protected HomeView()
    {
        // FOR EF!
    }

    private HomeView
        (string title, ViewType type, int ordering, bool isActive)
    {
        Title = title;
        Type = type;
        Ordering = ordering;
        IsActive = isActive;
    }


    public string Title { get; private set; }
    public ViewType Type { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsSystemic { get; private set; }

    public DateTime? DeactivateDate { get; private set; }

    public List<SlideViewItem> SliderViews { get; private set; }
    public List<ProductViewItem> ProductViews { get; private set; } = new();
    public List<ImageViewItem> ImageViews { get; private set; }

    public static HomeView Create
        (string title, ViewType type, int ordering, bool isActive = true)
    {
        var homeView = new HomeView(
            title.Fix() ?? "",
            type,
            ordering.NotNegativeInt(nameof(Ordering)),
            isActive);

        switch (type)
        {
            case ViewType.Slider:
                homeView.SliderViews = new();
                break;
            case ViewType.Product:
                homeView.ProductViews = new();
                break;
            case ViewType.Image:
                homeView.ImageViews = new();
                break;
            default:
                throw new InvalidOperationException("Unknown view type.");
        }

        return homeView;
    }

    public void Update(string title, int ordering)
    {
        Title = title.Fix() ?? "";
        Ordering = ordering.NotNegativeInt(nameof(Ordering));

        SetUpdateDateTime();
    }

    public bool AddSlide(SlideViewItem slide)
    {
        if (Type != ViewType.Slider)
        {
            return false;
        }

        SliderViews.Add(slide);

        return true;
    }

    public bool AddProduct(ProductViewItem product)
    {
        if (Type != ViewType.Product)
        {
            return false;
        }

        ProductViews.Add(product);

        return true;
    }

    public bool AddImage(ImageViewItem image)
    {
        if (Type != ViewType.Image)
        {
            return false;
        }

        ImageViews.Add(image);

        return true;
    }

    public void Activate()
    {
        IsActive = true;
        RemoveDeactivateTime();
    }

    public void Deactivate()
    {
        IsActive = false;
        SetDeactivateTime();
    }


    public void SetDeactivateTime()
    {
        //DeactivateDate =
        //    DateTimeUtility.GetCurrentUnixUTCTimeSeconds();

        DeactivateDate =
            DateTime.UtcNow;
    }

    public void RemoveDeactivateTime()
    {
        //DeactivateDate = 0;

        DeactivateDate = null;
    }

    public void MakeSystemic()
    {
        IsSystemic = true;
    }
}
