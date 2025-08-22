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
public class EditModel : BasePageModel
{
    private readonly SiteSettingsApplication _siteSettingsApplication;
    private readonly PriceListsApplication _priceListsApplication;
    private readonly ILogger<EditModel> _logger;

    public EditModel(
        SiteSettingsApplication siteSettingsApplication, 
        PriceListsApplication priceListsApplication,
        ILogger<EditModel> logger)
    {
        _siteSettingsApplication = siteSettingsApplication;
        _priceListsApplication = priceListsApplication;
        _logger = logger;
    }

    [BindProperty]
    public UpdateSiteSettingViewModel SiteSetting { get; set; } = new();
    
    public List<PriceListViewModel> PriceLists { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        try
        {
            _logger.LogInformation("Loading SiteSetting {Id} for editing", id);
            
            var siteSetting = await _siteSettingsApplication.GetSiteSettingByIdAsync(id);
            if (siteSetting == null)
            {
                _logger.LogWarning("SiteSetting {Id} not found", id);
                return NotFound();
            }

            // Map to UpdateSiteSettingViewModel
            SiteSetting = new UpdateSiteSettingViewModel
            {
                Id = siteSetting.Id,
                Name = siteSetting.Name,
                PhoneNumber = siteSetting.PhoneNumber,
                Description = siteSetting.Description,
                Address = siteSetting.Address,
                LogoURL = siteSetting.LogoURL,
                PriceListID = siteSetting.PriceListID
            };

            _logger.LogInformation("Loading PriceLists for Edit page");
            PriceLists = await _priceListsApplication.GetAllPriceListAsync();
            _logger.LogInformation("Loaded {Count} PriceLists", PriceLists.Count);
            
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading SiteSetting {Id} for editing", id);
            return NotFound();
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
            _logger.LogInformation("Updating SiteSetting {Id}", SiteSetting.Id);
            await _siteSettingsApplication.UpdateSiteSettingAsync(SiteSetting);
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating SiteSetting {Id}", SiteSetting.Id);
            ModelState.AddModelError("", "خطا در ویرایش تنظیمات: " + ex.Message);
            
            try
            {
                PriceLists = await _priceListsApplication.GetAllPriceListAsync();
            }
            catch (Exception loadEx)
            {
                _logger.LogError(loadEx, "Error loading PriceLists after update error");
                PriceLists = new List<PriceListViewModel>();
            }
            
            return Page();
        }
    }
}
