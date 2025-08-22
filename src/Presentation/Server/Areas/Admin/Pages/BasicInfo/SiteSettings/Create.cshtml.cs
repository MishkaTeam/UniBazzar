using Application.Aggregates.SiteSettings;
using Application.Aggregates.PriceLists;
using Application.ViewModels.SiteSettings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Infrastructure;
using Application.Aggregates.PriceLists.ViewModels.PriceList;

namespace UniBazzar.Server.Areas.Admin.Pages.BasicInfo.SiteSettings;

[Authorize]
public class CreateModel : BasePageModel
{
    private readonly SiteSettingsApplication _siteSettingsApplication;
    private readonly PriceListsApplication _priceListsApplication;
    private readonly ILogger<CreateModel> _logger;

    public CreateModel(
        SiteSettingsApplication siteSettingsApplication, 
        PriceListsApplication priceListsApplication,
        ILogger<CreateModel> logger)
    {
        _siteSettingsApplication = siteSettingsApplication;
        _priceListsApplication = priceListsApplication;
        _logger = logger;
    }

    [BindProperty]
    public CreateSiteSettingViewModel SiteSetting { get; set; } = new();
    
    public List<PriceListViewModel> PriceLists { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            _logger.LogInformation("Loading PriceLists for SiteSettings Create page");
            PriceLists = await _priceListsApplication.GetAllPriceListAsync();
            _logger.LogInformation("Loaded {Count} PriceLists", PriceLists.Count);
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading PriceLists for SiteSettings Create page");
            // Don't return error page, just show empty list
            PriceLists = new List<PriceListViewModel>();
            return Page();
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            try
            {
                PriceLists = await _priceListsApplication.GetAllPriceListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading PriceLists in OnPost");
                PriceLists = new List<PriceListViewModel>();
            }
            return Page();
        }

        try
        {
            await _siteSettingsApplication.CreateSiteSettingAsync(SiteSetting);
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating SiteSetting");
            ModelState.AddModelError("", "خطا در ایجاد تنظیمات: " + ex.Message);
            try
            {
                PriceLists = await _priceListsApplication.GetAllPriceListAsync();
            }
            catch (Exception loadEx)
            {
                _logger.LogError(loadEx, "Error loading PriceLists after creation error");
                PriceLists = new List<PriceListViewModel>();
            }
            return Page();
        }
    }
}
