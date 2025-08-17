using Modules.Inventory.Domain.Aggreates.Suppliers.Enums;

namespace Modules.Inventory.Application.Aggregates.Suppliers.ViewModels;

public class SupplierViewModel : CreateSupplierViewModel
{
    public Guid Id { get; set; }
}
