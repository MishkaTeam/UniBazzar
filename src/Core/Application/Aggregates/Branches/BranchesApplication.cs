using Application.Aggregates.Branches.ViewModels;
using Domain;
using Domain.Aggregates.branches;
using Mapster;

namespace Application.Aggregates.Branches;

public class BranchesApplication(IbranchRepository branchRepository, IUnitOfWork unitOfWork)
{
    public async Task<Branch> CreateAsync(CreateBranchViewModel viewModel)
    {
        var branch = Branch.Create
            (
            viewModel.Name
            );

        await branchRepository.AddAsync(branch);
        await unitOfWork.CommitAsync();

        return branch.Adapt<Branch>();
    }

    public async Task<UpdateBranchViewModel> GetBranchAsync(Guid id)
    {
        var branch = await branchRepository.GetByIdAsync(id);

        if (branch == null || branch.Id == Guid.Empty)
        {
            throw new Exception(Resources.Messages.Errors.NotFound);
        }
        return branch.Adapt<UpdateBranchViewModel>();
    }

    public async Task<List<UpdateBranchViewModel>> GetAllBranchesAsync()
    {
        var branch = await branchRepository.GetAllAsync();

        return branch.Adapt<List<UpdateBranchViewModel>>();
    }
    public async Task<UpdateBranchViewModel> UpdateAsync(UpdateBranchViewModel updateViewModel)
    {
        var branch = await branchRepository.GetByIdAsync(updateViewModel.Id);

        if (branch == null || branch.Id == Guid.Empty)
        {
            throw new Exception(Resources.Messages.Errors.NotFound);
        }

        branch.Update
            (
             updateViewModel.Name
            );

        await unitOfWork.CommitAsync();

        return branch.Adapt<UpdateBranchViewModel>();
    }
    public async Task DeleteAsync(Guid id)
    {
        var branch = await branchRepository.GetByIdAsync(id);

        if (branch == null || branch.Id == Guid.Empty)
        {
            throw new Exception(Resources.Messages.Errors.NotFound);
        }

        await branchRepository.RemoveAsync(branch);
        await unitOfWork.CommitAsync();
    }
}
