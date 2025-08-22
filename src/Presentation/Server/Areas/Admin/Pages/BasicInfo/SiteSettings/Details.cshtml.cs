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
public class DetailsModel : BasePageModel
{
    private readonly SiteSettingsApplication _siteSettingsApplication;
    private readonly PriceListsApplication _priceListsApplication;
    private readonly ILogger<DetailsModel> _logger;

    public DetailsModel(
        SiteSettingsApplication siteSettingsApplication, 
        PriceListsApplication priceListsApplication,
        ILogger<DetailsModel> logger)
    {
        _siteSettingsApplication = siteSettingsApplication;
        _priceListsApplication = priceListsApplication;
        _logger = logger;
    }

    public SiteSettingViewModel? SiteSetting { get; set; }
    public List<PriceListViewModel> PriceLists { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        try
        {
            _logger.LogInformation("Loading SiteSetting {Id} for details", id);
            
            SiteSetting = await _siteSettingsApplication.GetSiteSettingByIdAsync(id);
            if (SiteSetting == null)
            {
                _logger.LogWarning("SiteSetting {Id} not found", id);
                return NotFound();
            }

            _logger.LogInformation("Loading PriceLists for Details page");
            PriceLists = await _priceListsApplication.GetAllPriceListAsync();
            _logger.LogInformation("Loaded {Count} PriceLists", PriceLists.Count);
            
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading SiteSetting {Id} for details", id);
            return NotFound();
        }
    }
}
