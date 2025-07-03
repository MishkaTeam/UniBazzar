using Application.Aggregates.Attribute.ViewModels;
using Domain;
using Domain.Aggregates.Attributes.Data;
using Mapster;

namespace Application.Aggregates.Attribute;

public class AttributeApplication(IAttributeRepository attributeRepository, IUnitOfWork unitOfWork)
{
    public async Task<Domain.Aggregates.Attributes.Attribute> CreateAsync(CreateAttributeViewModel viewModel)
    {
        var attribute = Domain.Aggregates.Attributes.Attribute.Register
            (
            viewModel.Name,
            viewModel.Description,
            viewModel.CategoryId
            );

        await attributeRepository.AddAsync(attribute);
        await unitOfWork.SaveChangesAsync();

        return attribute.Adapt<Domain.Aggregates.Attributes.Attribute>();
    }

    public async Task<UpdateAttributeViewModel> GetAttributeAsync(Guid id)
    {
        var attribute = await attributeRepository.GetByIdAsync(id);

        if (attribute == null || attribute.Id == Guid.Empty)
        {
            throw new Exception(Resources.Messages.Errors.NotFound);
        }
        return attribute.Adapt<UpdateAttributeViewModel>();
    }

    public async Task<List<UpdateAttributeViewModel>> GetAllAttributeAsync()
    {
        var attribute = await attributeRepository.GetAllAsync();

        return attribute.Adapt<List<UpdateAttributeViewModel>>();
    }
    public async Task<UpdateAttributeViewModel> UpdateAsync(UpdateAttributeViewModel updateViewModel)
    {
        var attribute = await attributeRepository.GetByIdAsync(updateViewModel.Id);

        if (attribute == null || attribute.Id == Guid.Empty)
        {
            throw new Exception(Resources.Messages.Errors.NotFound);
        }

        attribute.Update
            (
             updateViewModel.Name,
             updateViewModel.Description,
             updateViewModel.CategoryId
            );

        await unitOfWork.SaveChangesAsync();

        return attribute.Adapt<UpdateAttributeViewModel>();
    }
    public async Task DeleteAsync(Guid id)
    {
        var attribute = await attributeRepository.GetByIdAsync(id);

        if (attribute == null || attribute.Id == Guid.Empty)
        {
            throw new Exception(Resources.Messages.Errors.NotFound);
        }

        await attributeRepository.RemoveAsync(attribute);
        await unitOfWork.SaveChangesAsync();
    }
}
