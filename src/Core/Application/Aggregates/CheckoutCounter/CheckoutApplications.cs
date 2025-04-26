using Application.Aggregates.CheckoutCounter.ViewModels;
using Domain;
using Domain.Aggregates.CheckoutCounter;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Aggregates.CheckoutCounter
{
    public class CheckoutApplications(ICheckoutCounterRepository checkoutCounterRepository,ICheckoutCounterOfWork checkoutCounterOfWork)
    {
        public async Task<CheckoutCounterViewModels> CreateAsync(CreateCheckoutCounterViewModels viewModel)
        {
            var entity = Domain.Aggregates.CheckoutCounter.CheckoutCounter.Create
                (viewModel.Name, viewModel.BasicCheckoutCounterID);
            checkoutCounterRepository.AddCheckoutCounter(entity);
            await checkoutCounterOfWork.CommitAsync();
            return entity.Adapt<CheckoutCounterViewModels>();
        }
        public async Task<List<CheckoutCounterViewModels>> GetRootCheckoutCounter()
        {
            var RootCheckoutCounter=await checkoutCounterRepository.GetAllCheckoutCountersAsync();
            return RootCheckoutCounter.Adapt<List<CheckoutCounterViewModels>>();
        }
        public async Task<List<CheckoutCounterViewModels>> GetCheckoutCounters()
        {
            var RootCheckoutCounter = await checkoutCounterRepository.GetAllCheckoutCountersAsync();
            return RootCheckoutCounter.Adapt<List<CheckoutCounterViewModels>>();
        }
        public async Task<CheckoutCounterViewModels> GetCheckoutCounterViewModels(Guid Id)
        {
            var CheckoutCounter = await checkoutCounterRepository.GetCheckoutCounterAsync(Id);
            var ViewModel = CheckoutCounter.Adapt<CheckoutCounterViewModels>();
            ViewModel.BasicCheckoutCounterID = Guid.Empty;
            return ViewModel;
        }

        public async Task<CheckoutCounterViewModels> UpdateAsync(CheckoutCounterViewModels UpdateViewModel)
        {
            var entity=await checkoutCounterRepository.GetCheckoutCounterAsync(UpdateViewModel.Id);
            if (entity == null || entity.Id == Guid.Empty)
            {
                throw new Exception(Resources.Messages.Errors.NotFound);
            }
            entity.Update(UpdateViewModel.Name,
                UpdateViewModel.BasicCheckoutCounterID);
            await checkoutCounterOfWork.CommitAsync();
            return entity.Adapt<CheckoutCounterViewModels>();
        }
        public async Task DeleteAsync(Guid id)
        {
            var entity=await checkoutCounterRepository.GetCheckoutCounterAsync(id);
            if (entity ==null ||entity.Id==Guid.Empty)
            {
                throw new Exception(Resources.Messages.Errors.NotFound);
            }
            checkoutCounterRepository.Remove(entity);
            await checkoutCounterOfWork.CommitAsync();
        }
    }
}
