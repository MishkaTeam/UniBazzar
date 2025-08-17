using Mapster;
using Modules.Inventory.Application.Aggregates.Warehouses.ViewModels;
using Modules.Inventory.Domain;
using Modules.Inventory.Domain.Aggreates.Warehouses;

namespace Modules.Inventory.Application.Aggregates.Warehouses;

public class WarehouseApplication(IWarehouseRepository warehouseRepository, IUnitOfWork unitOfWork)
{

    public async Task<CreateWarehouseViewModel> CreateAsync(WarehouseViewModel model)
    {
        var warehouse = Warehouse.Create(model.Name, model.Location);

        await warehouseRepository.AddAsync(warehouse);

        await unitOfWork.SaveChangesAsync();

        return warehouse.Adapt<CreateWarehouseViewModel>();
    }

    public async Task<List<WarehouseViewModel>> GetAllAsync()
    {
        var warehouse = await warehouseRepository.GetAllAsync();

        return warehouse.Adapt<List<WarehouseViewModel>>();
    }

    public async Task<WarehouseViewModel> GetWarehouseAsync(Guid Id)
    {
        var warehouse = await warehouseRepository.GetByIdAsync(Id);

        return warehouse.Adapt<WarehouseViewModel>();
    }

    public async Task DeleteAsync(Guid Id)
    {
        var warehouse = await warehouseRepository.GetByIdAsync(Id);

        if (warehouse == null || warehouse.Id == Guid.Empty)
        {
            throw new Exception(Resources.Messages.Errors.NotFound);
        }

        await warehouseRepository.RemoveAsync(warehouse);

        await unitOfWork.SaveChangesAsync();
    }

    public async Task<WarehouseViewModel> UpdateAsync(WarehouseViewModel model)
    {
        var warehouse = await warehouseRepository.GetByIdAsync(model.Id);

        if (warehouse == null || warehouse.Id == Guid.Empty)
        {
            throw new Exception(Resources.Messages.Errors.NotFound);
        }

        warehouse.Update(model.Name, model.Location);

        await unitOfWork.SaveChangesAsync();

        return warehouse.Adapt<WarehouseViewModel>();
    }

}
