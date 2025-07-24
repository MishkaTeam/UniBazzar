using Application.Aggregates.HomeViews;
using Application.Aggregates.HomeViews.ViewModels;
using Domain.Aggregates.Cms.HomeViews.Enums;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Resources;

namespace Server.Areas.Admin.Pages.BasicInfo.HomeViews;

public class CreateModel
    (HomeViewsApplication homeViewsApplication) : BasePageModel
{
    [BindProperty]
    public CreateHomeViewViewModel CreateViewModel { get; set; } = new();
    public List<SelectListItem> ViewTypeList { get; set; } = [];

    public void OnGet()
    {
        FillSelectTag();
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            FillSelectTag();

            return Page();
        }

        var result =
            await homeViewsApplication.AddHomeView(CreateViewModel);

        return RedirectToPage("Index");
    }


    private void FillSelectTag()
    {
        foreach (ViewType type in Enum.GetValues(typeof(ViewType)))
        {
            var typeNmae = type switch
            {
                ViewType.Slider => DataDictionary.Slider,
                ViewType.Product => DataDictionary.ProductView,
                ViewType.Image => DataDictionary.ImageView,
                _ => throw new InvalidOperationException("Unknown view type.")
            };

            ViewTypeList.Add(new SelectListItem()
            {
                Text = typeNmae,
                Value = ((byte)type).ToString(),
            });
        }
    }
}
