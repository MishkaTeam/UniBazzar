using Application.Aggregates.HomeViews;
using Application.Aggregates.HomeViews.ViewModels;
using Application.Aggregates.Stores;
using Domain.Aggregates.Cms.HomeViews.Enums;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Resources;

namespace Server.Areas.Admin.Pages.BasicInfo.HomeViews;

public class UpdateModel
    (HomeViewsApplication homeViewsApplication) : BasePageModel
{
    [BindProperty]
    public HomeViewViewModel UpdateViewModel { get; set; } = new();
    public List<SelectListItem> ViewTypeList { get; set; } = [];

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            return RedirectToPage("Index");
        }

        UpdateViewModel =
            (await homeViewsApplication.GetHomeViewAsync(id)).Data!;

        if (UpdateViewModel == null)
        {
            return RedirectToPage("Index");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            FillSelectTag();

            return Page();
        }

        var result =
            await homeViewsApplication.UpdateHomeView(UpdateViewModel);

        if (result.IsSuccessful == false)
        {
            AddPageError
                (result.ErrorMessage!.Message);

            FillSelectTag();

            return Page();
        }

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
