using Application.Aggregates.Units.ViewModels;
using Domain;
using Domain.Aggregates.Units;
using Mapster;

namespace Application.Aggregates.Units;

public class UnitsApplication(IUnitRepository unitRepository, IUnitOfWork unitOfWork)
{
    public async Task<UnitViewModel> CreateAsync(CreateUnitViewModel viewModel)
    {
        var unit = Unit.Create(viewModel.Title,
                                 viewModel.BaseUnitId,
                                 viewModel.Ratio);

        unitRepository.AddAsync(unit);

        await unitOfWork.SaveChangesAsync();

        return unit.Adapt<UnitViewModel>();
    }

    public async Task<List<UnitViewModel>> GetRootUnits()
    {
        var unit = await unitRepository.GetAllAsync();
        return unit.Adapt<List<UnitViewModel>>();
    }
    public async Task<List<UnitViewModel>> GetUnits()
    {
        var unit = await unitRepository.GetAllAsync();
        return unit.Adapt<List<UnitViewModel>>();
    }

    public async Task<UnitViewModel> GetUnitAsync(Guid Id)
    {
        var unit = await unitRepository.GetByIdAsync(Id);
        var viewModel = unit.Adapt<UnitViewModel>();

        viewModel.BaseUnitId ??= Guid.Empty;

        return viewModel;
    }

    public async Task<UnitViewModel> UpdateAsync(UnitViewModel updateViewModel)
    {
        var unit = await unitRepository.GetByIdAsync(updateViewModel.Id);

        if (unit == null || unit.Id == Guid.Empty)
        {
            throw new Exception(Resources.Messages.Errors.NotFound);
        }

        unit.Update(updateViewModel.Title,
                updateViewModel.BaseUnitId,
                updateViewModel.Ratio);

        await unitOfWork.SaveChangesAsync();
        return unit.Adapt<UnitViewModel>();
    }

    public async Task DeleteAsync(Guid id)
    {
        var unit = await unitRepository.GetByIdAsync(id);

        if (unit == null || unit.Id == Guid.Empty)
        {
            throw new Exception(Resources.Messages.Errors.NotFound);
        }

        await unitRepository.RemoveAsync(unit);
        await unitOfWork.SaveChangesAsync();
    }
}
