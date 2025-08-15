using Application.Aggregates.PosSettings.ViewModels;
using Application.Aggregates.Units.ViewModels;
using Domain;
using Domain.Aggregates.PosSettings;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Aggregates.PosSettings
{
    public class PosSettingsApplication(IPosSettingRepository posSettingRepository,IUnitOfWork unitOfWork)
    {
        public async Task<PosSettingsViewModel>CreateAsync(CreatePosSettingsViewModel viewModel)
        {
            var posSetting = PosSetting.Create(viewModel.Title, viewModel.Description, viewModel.PhoneNumber, viewModel.LogoUrl
                , viewModel.PriceListId, viewModel.PublicCustomer, viewModel.Address);
            await posSettingRepository.AddAsync(posSetting);
            await unitOfWork.SaveChangesAsync();
            return posSetting.Adapt<PosSettingsViewModel>();

                
        }
    }
}
