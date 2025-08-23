using Application.Aggregates.SiteSettings;
using Application.Aggregates.SiteSettings.ViewModels;
using Application.Aggregates.PriceLists;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Resources;

namespace Server.Areas.Admin.Pages.BasicInfo.SiteSettings;

public class CreateModel(SiteSettingApplication siteSettingApplication, PriceListsApplication priceListsApplication) : BasePageModel
{
    [BindProperty]
    public CreateSiteSettingViewModel CreateViewModel { get; set; } = new();
    public List<SelectListItem> PriceListOptions { get; set; } = [];

    public async Task OnGet()
    {
        await FillPriceListOptions();
    }

    public async Task<IActionResult> OnPost()
    {
        await FillPriceListOptions();
        if (ModelState.IsValid)
        {
            try
            {
                await siteSettingApplication.CreateSiteSettingAsync(CreateViewModel);
                TempData["SuccessMessage"] = string.Format(Resources.Messages.Successes.Created, DataDictionary.SiteSetting);
                return RedirectToPage("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
        }
        return Page();
    }

    private async Task FillPriceListOptions()
    {
        var priceLists = await priceListsApplication.GetAllPriceListAsync();
        
        PriceListOptions = new List<SelectListItem>
        {
            new SelectListItem { Text = "انتخاب کنید", Value = "" }
        };
        
        PriceListOptions.AddRange(priceLists.Select(x => new SelectListItem
        {
            Text = x.Title,
            Value = x.Id.ToString()
        }).ToList());
    }
}
