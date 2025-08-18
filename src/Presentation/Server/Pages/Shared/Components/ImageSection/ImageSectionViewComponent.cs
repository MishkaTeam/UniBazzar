using Application.Aggregates.HomeViews.ViewModels.HomeViews;
using Domain.Aggregates.Cms.HomeViews.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Server.Pages.Shared.Components.ImageSection;

public class ImageSectionViewComponent : ViewComponent
{
    public static string KEY = "ImageSection";

    public IViewComponentResult Invoke
        (IndexHomeViewViewModel homeView)
    {
        if (homeView.IsActive == false ||
            homeView.Type != ViewType.Image)
        {
            return null!;
        }

        homeView.ImageViews =
            homeView.ImageViews.OrderBy(x => x.Ordering).ToList();

        return View("Default", homeView);
    }
}
