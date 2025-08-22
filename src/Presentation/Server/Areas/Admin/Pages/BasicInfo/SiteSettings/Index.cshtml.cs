using Application.Aggregates.SiteSettings;
using Application.Aggregates.PriceLists;
using Application.ViewModels.SiteSettings;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Infrastructure;
using Application.Aggregates.PriceLists.ViewModels.PriceList;

namespace UniBazzar.Server.Areas.Admin.Pages.BasicInfo.SiteSettings;

[Authorize]
public class IndexModel : BasePageModel
{
    private readonly SiteSettingsApplication _siteSettingsApplication;
    private readonly PriceListsApplication _priceListsApplication;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(
        SiteSettingsApplication siteSettingsApplication, 
        PriceListsApplication priceListsApplication,
        ILogger<IndexModel> logger)
    {
        _siteSettingsApplication = siteSettingsApplication;
        _priceListsApplication = priceListsApplication;
        _logger = logger;
    }

    public List<SiteSettingViewModel> SiteSettings { get; set; } = new();
    public List<PriceListViewModel> PriceLists { get; set; } = new();

    public async Task OnGetAsync()
    {
        try
        {
            _logger.LogInformation("Loading SiteSettings and PriceLists for Index page");
            
            var siteSettingsTask = _siteSettingsApplication.GetAllSiteSettingsAsync();
            var priceListsTask = _priceListsApplication.GetAllPriceListAsync();
            
            await Task.WhenAll(siteSettingsTask, priceListsTask);
            
            SiteSettings = siteSettingsTask.Result;
            PriceLists = priceListsTask.Result;
            
            _logger.LogInformation("Loaded {SiteSettingsCount} SiteSettings and {PriceListsCount} PriceLists", 
                SiteSettings.Count, PriceLists.Count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading data for SiteSettings Index page");
            SiteSettings = new List<SiteSettingViewModel>();
            PriceLists = new List<PriceListViewModel>();
        }
    }
}
