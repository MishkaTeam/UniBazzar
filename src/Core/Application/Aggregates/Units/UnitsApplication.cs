using Application.Aggregates.Units.ViewModels;
using Domain;
using Domain.Aggregates.Units;
using Mapster;

namespace Application.Aggregates.Units;

public class UnitsApplication(IUnitRepository unitRepository, IUnitOfWork unitOfWork)
{
	public async Task<UnitViewModel> CreateAsync(CreateUnitViewModel viewModel)
	{
		var entity = Unit.Create(viewModel.Title,
								 viewModel.BaseUnitId,
								 viewModel.Ratio);

		unitRepository.AddUnit(entity);
		await unitOfWork.CommitAsync();
		return entity.Adapt<UnitViewModel>();
	}

	public async Task<List<UnitViewModel>> GetRootUnits()
	{
		var rootUnits = await unitRepository.GetRootUnitsAsync();
		return rootUnits.Adapt<List<UnitViewModel>>();
	}
	public async Task<List<UnitViewModel>> GetUnits()
	{
		var rootUnits = await unitRepository.GetAllUnitsAsync();
		return rootUnits.Adapt<List<UnitViewModel>>();
	}

	public async Task<UnitViewModel> GetUnitAsync(Guid Id)
	{
		var unit = await unitRepository.GetUnitAsync(Id);
		var viewModel = unit.Adapt<UnitViewModel>();
		viewModel.BaseUnitId ??= Guid.Empty;
		return viewModel;
	}


	public async Task<UnitViewModel> UpdateAsync(UnitViewModel updateViewModel)
	{
		var entity = await unitRepository.GetUnitAsync(updateViewModel.Id);

		if (entity == null || entity.Id == Guid.Empty)
		{
			throw new Exception(Resources.Messages.Errors.NotFound);
		}

		entity.Update(updateViewModel.Title,
				updateViewModel.BaseUnitId,
				updateViewModel.Ratio);

		await unitOfWork.CommitAsync();
		return entity.Adapt<UnitViewModel>();
	}

	public async Task DeleteAsync(Guid id)
	{
		var entity = await unitRepository.GetUnitAsync(id);

		if (entity == null || entity.Id == Guid.Empty)
		{
			throw new Exception(Resources.Messages.Errors.NotFound);
		}

		unitRepository.Remove(entity);
		await unitOfWork.CommitAsync();
	}
}
