using Application.Aggregates.CheckoutCounter.ViewModels;
using Domain;
using Domain.Aggregates.CheckoutCounters;
using Mapster;

namespace Application.Aggregates.CheckoutCounter
{
    public class CheckoutCounterApplication(ICheckoutCounterRepository checkoutCounterRepository, IUnitOfWork unitofWork)
    {
        public async Task<CheckoutCounterViewModels> CreateAsync(CreateCheckoutCounterViewModels viewModel)
        {
            var entity = Domain.Aggregates.CheckoutCounters.CheckoutCounter.Create(viewModel.Name);
            await checkoutCounterRepository.AddAsync(entity);
            await unitofWork.SaveChangesAsync();
            return entity.Adapt<CheckoutCounterViewModels>();
        }

        public async Task<List<CheckoutCounterViewModels>> GetCheckoutCounters()
        {
            var RootCheckoutCounter = await checkoutCounterRepository.GetAllAsync();
            return RootCheckoutCounter.Adapt<List<CheckoutCounterViewModels>>();
        }
        public async Task<CheckoutCounterViewModels> GetCheckoutCounterViewModels(Guid Id)
        {
            var CheckoutCounter = await checkoutCounterRepository.GetByIdAsync(Id);
            var ViewModel = CheckoutCounter.Adapt<CheckoutCounterViewModels>();
            return ViewModel;
        }

        public async Task<CheckoutCounterViewModels> UpdateAsync(CheckoutCounterViewModels UpdateViewModel)
        {
            var entity = await checkoutCounterRepository.GetByIdAsync(UpdateViewModel.Id);
            if (entity == null || entity.Id == Guid.Empty)
            {
                throw new Exception(Resources.Messages.Errors.NotFound);
            }
            entity.Update(UpdateViewModel.Name);
            await unitofWork.SaveChangesAsync();
            return entity.Adapt<CheckoutCounterViewModels>();
        }
        public async Task DeleteAsync(Guid id)
        {
            await checkoutCounterRepository.RemoveByIdAsync(id);
            await unitofWork.SaveChangesAsync();
        }
    }
}
