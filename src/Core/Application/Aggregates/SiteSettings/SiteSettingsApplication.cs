using Application.ViewModels.SiteSettings;
using Domain;
using Domain.Aggregates.SiteSettings;
using Mapster;
using Resources.Messages;

namespace Application.Aggregates.SiteSettings;

public class SiteSettingsApplication
{
    private readonly ISiteSettingRepository _siteSettingRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SiteSettingsApplication(ISiteSettingRepository siteSettingRepository, IUnitOfWork unitOfWork)
    {
        _siteSettingRepository = siteSettingRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<SiteSettingViewModel> CreateSiteSettingAsync(CreateSiteSettingViewModel viewModel)
    {
        var siteSetting = SiteSetting.Create(
            viewModel.Description,
            viewModel.Name,
            viewModel.PhoneNumber,
            viewModel.LogoURL,
            viewModel.PriceListID,
            viewModel.Address
        );

        await _siteSettingRepository.AddAsync(siteSetting);
        await _unitOfWork.SaveChangesAsync();

        return siteSetting.Adapt<SiteSettingViewModel>();
    }

    public async Task<List<SiteSettingViewModel>> GetAllSiteSettingsAsync()
    {
        var siteSettings = await _siteSettingRepository.GetAllAsync();
        return siteSettings.Adapt<List<SiteSettingViewModel>>();
    }

    public async Task<SiteSettingViewModel?> GetSiteSettingByIdAsync(Guid id)
    {
        var siteSetting = await _siteSettingRepository.GetByIdAsync(id);
        return siteSetting?.Adapt<SiteSettingViewModel>();
    }

    public async Task<SiteSettingViewModel?> GetSiteSettingByStoreIdAsync(Guid storeId)
    {
        var siteSetting = await _siteSettingRepository.GetAsync(x => x.StoreId == storeId);
        return siteSetting?.Adapt<SiteSettingViewModel>();
    }

    public async Task<SiteSettingViewModel> UpdateSiteSettingAsync(UpdateSiteSettingViewModel viewModel)
    {
        var siteSetting = await _siteSettingRepository.GetByIdAsync(viewModel.Id);
        
        if (siteSetting == null)
        {
            var message = string.Format(Errors.NotFound, Resources.DataDictionary.SiteSetting);
            throw new Exception(message);
        }

        siteSetting.Update(
            viewModel.Description,
            viewModel.Name,
            viewModel.PhoneNumber,
            viewModel.LogoURL,
            viewModel.PriceListID,
            viewModel.Address
        );

        await _unitOfWork.SaveChangesAsync();
        return siteSetting.Adapt<SiteSettingViewModel>();
    }

    public async Task DeleteSiteSettingAsync(Guid id)
    {
        var siteSetting = await _siteSettingRepository.GetByIdAsync(id);
        
        if (siteSetting == null)
        {
            var message = string.Format(Errors.NotFound, Resources.DataDictionary.SiteSetting);
            throw new Exception(message);
        }

        await _siteSettingRepository.RemoveAsync(siteSetting);
        await _unitOfWork.SaveChangesAsync();
    }
}
