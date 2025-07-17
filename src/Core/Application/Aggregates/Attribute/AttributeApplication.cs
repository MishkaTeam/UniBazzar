using Application.Aggregates.Attribute.ViewModels;
using Application.Aggregates.Attribute.ViewModels.AttributeValues;
using Domain;
using Domain.Aggregates.Attributes;
using Domain.Aggregates.Attributes.Data;
using Framework.DataType;
using Mapster;

namespace Application.Aggregates.Attribute;

public class AttributeApplication(IAttributeRepository attributeRepository, IUnitOfWork unitOfWork)
{
    public async Task<AttributeViewModel> CreateAsync(CreateAttributeViewModel viewModel)
    {
        var attribute = Domain.Aggregates.Attributes.Attribute.Create
            (
            viewModel.Name,
            viewModel.Description,
            viewModel.CategoryId
            );

        await attributeRepository.AddAsync(attribute);
        await unitOfWork.SaveChangesAsync();

        return attribute.Adapt<AttributeViewModel>();
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

    public async Task<ResultContract<AttributeViewModel>> AddValue(CreateAttributeItemRequestModel ViewModel)
    {
        var attribute = await attributeRepository.GetByIdAsync(ViewModel.AttributeId);
        var attributeValue = AttributeValue.Create
            (
            ViewModel.Name,
            ViewModel.PriceAdjustment,
            ViewModel.WeightAdjustment,
            ViewModel.IsPreSelected
        );

        attribute.AddValue(attributeValue);
        await unitOfWork.SaveChangesAsync();
        return AttributeViewModel.FromAttribute(attribute);

    }

    public async Task<ResultContract<List<AttributeValueViewModel>>> GetAttributeValues(Guid attributeId)
    {
        var attribute = await attributeRepository.GetByIdAsync(attributeId);
        var attributeValues = attribute.AttributeValues.Select(x => new AttributeValueViewModel()
        {
            AttributeId=attributeId,
            Id=x.Id,
            Name = x.Name,
            PriceAdjustment = x.PriceAdjustment,
            WeightAdjustment = x.WeightAdjustment,
            IsPreSelected = x.IsPreSelected

        }).ToList();
        return attributeValues;
    }

    public async Task<ResultContract<AttributeValueViewModel>> GetAttributeValue(Guid attributeId, Guid attributeValueId)
    {
        var attribute = await attributeRepository.GetByIdAsync(attributeId);
        var attributeValue = attribute.AttributeValues.FirstOrDefault(x => x.Id == attributeValueId);
        return attributeValue.Adapt<AttributeValueViewModel>();
    }

    public async Task<ResultContract<AttributeValueViewModel>> UpdateAttributeValue(UpdateAttributeValueViewModel updateViewModel)
    {
        var attribute = await attributeRepository.GetByIdAsync(updateViewModel.AttributeId);

        var attributeValues = attribute.AttributeValues.FirstOrDefault(x => x.Id == updateViewModel.Id);

        attributeValues.Update
        (
            updateViewModel.Name,
            updateViewModel.PriceAdjustment,
            updateViewModel.WeightAdjustment,
            updateViewModel.IsPreSelected
        );

        await unitOfWork.SaveChangesAsync();

        return attributeValues.Adapt<AttributeValueViewModel>();
    }

    public async Task RemoveAttributeValue(Guid attributeId, Guid attributeValueId)
    {
        var attribute = await attributeRepository.GetByIdAsync(attributeId);
        var attributeValue = attribute.AttributeValues.FirstOrDefault(x => x.Id == attributeValueId);

        attribute.AttributeValues.Remove(attributeValue);

        await unitOfWork.SaveChangesAsync();
    }
}
