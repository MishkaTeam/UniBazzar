using Application.Aggregates.HomeViews.ViewModels.HomeViews;
using Domain.Aggregates.Cms.HomeViews.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Server.Pages.Shared.Components.SliderSection;

public class SliderSectionViewComponent : ViewComponent
{
    public static string KEY = "SliderSection";

    public IViewComponentResult Invoke
        (IndexHomeViewViewModel homeView)
    {
        if (homeView.IsActive == false ||
            homeView.Type != ViewType.Slider)
        {
            return null!;
        }

        homeView.SliderViews =
            homeView.SliderViews.OrderBy(x => x.Ordering).ToList();

        return View("Default", homeView);
    }
}