using Application.Aggregates.SiteSettings;
using Application.Aggregates.SiteSettings.ViewModels;
using Infrastructure;

namespace Server.Areas.Admin.Pages.BasicInfo.SiteSettings
{
    public class IndexModel(SiteSettingApplication siteSettingApplication) : BasePageModel
    {
        public List<SiteSettingViewModel> ViewModel { get; set; } = [];

        public async Task OnGet()
        {
            ViewModel = await siteSettingApplication.GetSiteSettings();
        }
    }
}
