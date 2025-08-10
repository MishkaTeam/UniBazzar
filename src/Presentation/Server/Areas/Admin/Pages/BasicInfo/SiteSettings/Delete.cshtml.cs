using Application.Aggregates.SiteSettings;
using Application.Aggregates.SiteSettings.ViewModels;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Resources;

namespace Server.Areas.Admin.Pages.BasicInfo.SiteSettings;

public class DeleteModel(SiteSettingApplication siteSettingApplication) : BasePageModel
{
    [BindProperty]
    public SiteSettingViewModel ViewModel { get; set; } = new();

    public async Task OnGet(Guid id)
    {
        ViewModel = await siteSettingApplication.GetSiteSettingAsync(id);
    }

    public async Task<IActionResult> OnPost()
    {
        try
        {
            await siteSettingApplication.DeleteSiteSettingAsync(ViewModel.ID);
            TempData["SuccessMessage"] = string.Format(Resources.Messages.Successes.Deleted, DataDictionary.SiteSetting);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }
        
        return RedirectToPage("Index");
    }
}
