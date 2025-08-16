using Domain.Aggregates.Units;
using Mapster;
using Modules.Inventory.Application.Aggregates.Suppliers.ViewModels;
using Modules.Inventory.Domain;
using Modules.Inventory.Domain.Aggreates.Suppliers;

namespace Modules.Inventory.Application.Aggregates.Suppliers;

public class SupplierApplication(ISupplierRepository supplierRepository, IUnitOfWork unitOfWork)
{

    public async Task<SupplierViewModel> CreateAsync(SupplierViewModel model)
    {
        var supplier = Supplier.Create(model.FullName, model.NationalID, model.EconomicCode,
                                       model.Phone, model.Address, model.SupplierType);

        await supplierRepository.AddAsync(supplier);

        await unitOfWork.SaveChangesAsync();

        return supplier.Adapt<SupplierViewModel>();

    }

    public async Task<List<SupplierViewModel>> GetAllSuplierAsync(SupplierViewModel model)
    {
        var supplier = await supplierRepository.GetAllAsync();

        return supplier.Adapt<List<SupplierViewModel>>();
    }

    public async Task<SupplierViewModel> GetSupplierAsync(Guid id)
    {
        var supplier = await supplierRepository.GetByIdAsync(id);

        return supplier.Adapt<SupplierViewModel>();
    }

    public async Task DeleteAsync(Guid id)
    {
        var supplier = await supplierRepository.GetByIdAsync(id);

        if (supplier == null || supplier.Id == Guid.Empty)
        {
            throw new Exception(Resources.Messages.Errors.NotFound);
        }

        await supplierRepository.RemoveAsync(supplier);

        await unitOfWork.SaveChangesAsync();
    }

    public async Task<SupplierViewModel> UpdateAsync(SupplierViewModel model)
    {
        var supplier = await supplierRepository.GetByIdAsync(model.Id);

        if (supplier == null || supplier.Id == Guid.Empty)
        {
            throw new Exception(Resources.Messages.Errors.NotFound);
        }

        supplier.UpdateDetails(model.FullName, model.NationalID, model.EconomicCode,
                               model.Phone, model.Address, model.SupplierType);

        await unitOfWork.SaveChangesAsync();

        return supplier.Adapt<SupplierViewModel>();

    }

}
