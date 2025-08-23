using Application.Aggregates.SiteSettings.ViewModels;
using Domain.Aggregates.SiteSettings;
using Domain;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Aggregates.SiteSettings
{
    public class SiteSettingApplication(ISiteSettingRepository siteSettingRepository, IUnitOfWork unitOfWork)
    {
        public async Task<SiteSettingViewModel> CreateSiteSettingAsync(CreateSiteSettingViewModel siteSettingViewModel)
        {
            var siteSetting = SiteSetting.Create(siteSettingViewModel.Name, siteSettingViewModel.Description
                , siteSettingViewModel.PhoneNumber, siteSettingViewModel.LogoURL
                , siteSettingViewModel.PriceListID, siteSettingViewModel.Address);
            await siteSettingRepository.AddAsync(siteSetting);
            await unitOfWork.SaveChangesAsync();
            return siteSetting.Adapt<SiteSettingViewModel>();
        }
        public async Task<List<SiteSettingViewModel>> GetSiteSettings()
        {
            var siteSettings = await siteSettingRepository.GetAllAsync();
            return siteSettings.Adapt<List<SiteSettingViewModel>>();
        }

        public async Task<SiteSettingViewModel> GetSiteSettingAsync(Guid id)
        {
            var siteSetting = await siteSettingRepository.GetByIdAsync(id);
            return siteSetting.Adapt<SiteSettingViewModel>();
        }

        public async Task<SiteSettingViewModel> UpdateSiteSettingAsync(SiteSettingViewModel updateViewModel)
        {
            var siteSettingForUpdate = await siteSettingRepository.GetByIdAsync(updateViewModel.ID);
            if (siteSettingForUpdate == null || siteSettingForUpdate.Id == Guid.Empty)
            {
                throw new Exception(string.Format(Resources.Messages.Errors.NotFound, Resources.DataDictionary.SiteSetting));
            }
            siteSettingForUpdate.Update(updateViewModel.Name, updateViewModel.Description
                , updateViewModel.PhoneNumber, updateViewModel.LogoURL
                , updateViewModel.PriceListID, updateViewModel.Address);
            await unitOfWork.SaveChangesAsync();
            return siteSettingForUpdate.Adapt<SiteSettingViewModel>();
        }
        public async Task DeleteSiteSettingAsync(Guid id)
        {
            var siteSetting = await siteSettingRepository.GetByIdAsync(id);
            if (siteSetting == null || siteSetting.Id == Guid.Empty)
            {
                throw new Exception(string.Format(Resources.Messages.Errors.NotFound, Resources.DataDictionary.SiteSetting));
            }
            await siteSettingRepository.RemoveByIdAsync(siteSetting.Id);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
