using Application.Aggregates.HomeViews;
using Application.Aggregates.HomeViews.ViewModels.HomeViews;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Areas.Admin.Pages.BasicInfo.HomeViews;

public class DeleteModel
    (HomeViewsApplication homeViewsApplication) : BasePageModel
{
    [BindProperty]
    public HomeViewViewModel DeleteViewModel { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            return RedirectToPage("Index");
        }

        DeleteViewModel =
            (await homeViewsApplication.GetHomeViewAsync(id)).Data!;

        if (DeleteViewModel == null)
        {
            return RedirectToPage("Index");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await homeViewsApplication.DeleteHomeView(DeleteViewModel.Id);

        return RedirectToPage("Index");
    }
}
